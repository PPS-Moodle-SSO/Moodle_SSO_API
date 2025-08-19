using Moodle_SSO_API.Services.Moodle.Models;

namespace Moodle_SSO_API.Services.Interfaces
{
    public interface IMoodleService
    {
        Task<GetUserByEmailResponse> GetUserByEmail(string moodleUrl, string token, string value);
        Task<AuthenticateResponse> Authenticate(string moodleUrl, string token, string email);
    }
}
