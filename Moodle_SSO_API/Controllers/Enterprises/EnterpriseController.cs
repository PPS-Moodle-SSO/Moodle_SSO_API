using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moodle_SSO_API.Controllers.Enterprises.Requests;
using Moodle_SSO_API.Controllers.Enterprises.Responses;
using Moodle_SSO_API.Exceptions;
using Moodle_SSO_API.Handlers.Enterprises.ModelsDto;
using Moodle_SSO_API.Handlers.IHandler;
using Moodle_SSO_API.Models;
using System.Net;

namespace Moodle_SSO_API.Controllers.Enterprises
{
    [Route("api/enterprise")]
    [ApiController]
    public class EnterpriseController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IEnterpriseHandler _enterpriseHandler;

        public EnterpriseController(ILogger<EnterpriseController> logger, IMapper mapper, IEnterpriseHandler handler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _enterpriseHandler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        [HttpPost("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse<UpdateEnterpriseResponse>>> UpdateEnterprise([FromBody] UpdateEnterpriseRequest request)
        {
            var _response = new APIResponse<UpdateEnterpriseResponse>();

            try
            {
                var updateEnterpriseRequestDto = _mapper.Map<UpdateEnterpriseRequestDto>(request);
                var enterprise = await _enterpriseHandler.UpdateEnterprise(updateEnterpriseRequestDto);

                var updateEnterpriseResponse = _mapper.Map<UpdateEnterpriseResponse>(enterprise);
                _response.Result = updateEnterpriseResponse;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (EnterpriseNotFoundException ex)
            {
                _logger.LogWarning(ex, "Enterprise no encontrada: {IdEnterprise}", request.IdEnterprise);
                _response.IsSuccessful = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorsMessage = new List<string> { ex.Message };
                return NotFound(_response);
            }
            catch (InvalidEnterpriseDataException ex)
            {
                _logger.LogWarning(ex, "Datos inválidos al actualizar la Enterprise con ID {IdEnterprise}", request.IdEnterprise);
                _response.IsSuccessful = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorsMessage = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la Enterprise con ID {IdEnterprise}", request.IdEnterprise);
                _response.IsSuccessful = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorsMessage = new List<string> { ex.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}
