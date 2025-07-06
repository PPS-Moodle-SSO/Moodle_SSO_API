using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moodle_SSO_API.Controllers.Moodle.Requests;
using Moodle_SSO_API.Controllers.Moodle.Responses;
using Moodle_SSO_API.Handlers.IHandler;
using Moodle_SSO_API.Handlers.Moodles.Models;
using Moodle_SSO_API.Models;
using System.Net;

namespace Moodle_SSO_API.Controllers.Moodle
{
    [Route("api/moodle")]
    [ApiController]
    public class MoodleController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IMoodleHandler _moodleHandler;
        private readonly IEnterpriseHandler _enterpriseHandler;

        public MoodleController(ILogger<MoodleController> logger, IMapper mapper, IMoodleHandler moodleHandler, IEnterpriseHandler enterpriseHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _moodleHandler = moodleHandler ?? throw new ArgumentNullException(nameof(moodleHandler));
            _enterpriseHandler = enterpriseHandler ?? throw new ArgumentNullException(nameof(enterpriseHandler));
        }

        [HttpPost("get-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse<GetUserResponse>>> GetUser([FromBody] GetUserRequest request)
        {
            var _response = new APIResponse<GetUserResponse>();
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var enterprise = await _enterpriseHandler.GetEnterpriseByDomain(request.Domain);
                if (enterprise == null)
                {
                    _logger.LogError("Enterprise not found for domain: {Domain}", request.Domain);
                    ModelState.AddModelError("Enterprise not found", "Enterprise not found for the provided domain");
                    return BadRequest(ModelState);
                }

                var getUserRequestDto = new GetUserRequestDto
                {
                    Enterprise = enterprise,
                    UserEmail = request.UserEmail
                };
                var getUserResponseDto = await _moodleHandler.TryGetUser(getUserRequestDto);

                if (getUserResponseDto == null)
                {
                    _logger.LogError("User not found for email: {Email}", request.UserEmail);
                    ModelState.AddModelError("User not found", "User not found for the provided email");
                    return NotFound(ModelState);
                }

                _logger.LogInformation("User found: {UserEmail}", request.UserEmail);
                
                var getUserResponse = _mapper.Map<GetUserResponse>(getUserResponseDto);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = getUserResponse;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _logger.LogError("Get user", ex.Message);
                _response.IsSuccessful = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorsMessage = new List<string>() { ex.ToString() };
                return _response;
            }
        }
    }
}
