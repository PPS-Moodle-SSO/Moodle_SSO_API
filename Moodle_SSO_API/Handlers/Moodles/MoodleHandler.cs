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
            var getUserByEmailResponse = await _moodleService.GetUserByEmail(
                enterprise.MoodleUrl,
                enterprise.MoodleApiKey,
                requestDto.UserEmail
            );

            // La respuesta es una lista, tomo el primer usuario si existe
            var firstUser = getUserByEmailResponse?.FirstOrDefault();
            if (firstUser != null)
            {
                var hasFirst = !string.IsNullOrWhiteSpace(firstUser.Firstname);
                var hasLast = !string.IsNullOrWhiteSpace(firstUser.Lastname);
                if ((!hasFirst || !hasLast) && !string.IsNullOrWhiteSpace(firstUser.Fullname))
                {
                    var parts = firstUser.Fullname.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    if (!hasFirst && parts.Length > 0)
                        firstUser.Firstname = parts[0];
                    if (!hasLast && parts.Length > 1)
                        firstUser.Lastname = string.Join(' ', parts.Skip(1));
                }
            }
            return firstUser;
        }

        public async Task<AuthenticateResponseDto?> Authenticate(AuthenticateRequestDto requestDto)
        {
            var authenticateResponse = await _moodleService.Authenticate(
                requestDto.Enterprise.MoodleUrl,
                requestDto.Enterprise.MoodleApiKey,
                requestDto.Email
            );
            var authenticateResponseDto = _mapper.Map<AuthenticateResponseDto>(authenticateResponse);

            return authenticateResponseDto;
        }
    }
}
