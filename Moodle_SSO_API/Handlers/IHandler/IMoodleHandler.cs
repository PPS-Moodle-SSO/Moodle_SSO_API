using Moodle_SSO_API.Handlers.Moodles.Models;

namespace Moodle_SSO_API.Handlers.IHandler
{
    public interface IMoodleHandler
    {
        Task<GetUserResponseDto?> TryGetUser(GetUserRequestDto requestDto);
        Task<AuthenticateResponseDto?> Authenticate(AuthenticateRequestDto requestDto);
    }
}
