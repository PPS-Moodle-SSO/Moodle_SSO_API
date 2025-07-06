using AutoMapper;
using Moodle_SSO_API.Handlers.IHandler;
using Moodle_SSO_API.Handlers.Moodles.Models;
using Moodle_SSO_API.Services.Interfaces;

namespace Moodle_SSO_API.Handlers.Moodles
{
    public class MoodleHandler : IMoodleHandler
    {
        private readonly IMoodleService _moodleService;
        private readonly IMapper _mapper;

        public MoodleHandler(IMoodleService moodleService, IMapper mapper)
        {
            _moodleService = moodleService ?? throw new ArgumentNullException(nameof(moodleService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<GetUserResponseDto?> TryGetUser(GetUserRequestDto requestDto)
        {
            var enterprise = requestDto.Enterprise;
            var getUserByEmailResponse = await _moodleService.GetUserByEmail(enterprise.MoodleUrl, enterprise.MoodleApiKey, requestDto.UserEmail);
            var getUserResponseDto = _mapper.Map<GetUserResponseDto>(getUserByEmailResponse);

            return getUserResponseDto;
        }
    }
}
