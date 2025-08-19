using Moodle_SSO_API.Helpers;
using Moodle_SSO_API.Services.HTTP.Models;
using Moodle_SSO_API.Services.Interfaces;
using Moodle_SSO_API.Services.Moodle.Models;
using System.Net;

namespace Moodle_SSO_API.Services.Moodle
{
    public class MoodleService : IMoodleService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        private string WSFunction { get; set; }
        private string MoodleWSRestFormat { get; set; }
        private string LocalMoodleUrl { get; set; }

        public MoodleService(IConfiguration config, IHttpService httpService)
        {
            _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            WSFunction = config["Moodle:wsfunction"] ?? throw new ArgumentNullException("WSFunction is not configured.");
            MoodleWSRestFormat = config["Moodle:moodlewsrestformat"] ?? throw new ArgumentNullException("MoodleWSRestFormat is not configured.");
            LocalMoodleUrl = config["Moodle:LocalUrl"] ?? "http://localhost:8888";
        }

        public async Task<GetUserByEmailResponse> GetUserByEmail(string moodleUrl, string token, string value)
        {
            GetUserByEmailResponse response;

            var url = $"{moodleUrl}/webservice/rest/server.php?wstoken={token}&wsfunction={WSFunction}&moodlewsrestformat={MoodleWSRestFormat}&field=email&values[0]={Uri.EscapeDataString(value)}";
            
            Console.WriteLine($"Calling URL: {url}");
            
            var httpResponse = await _httpService.SendGetRequest(url, null, 1);
            var httpResponseContent = await httpResponse?.Content?.ReadAsStringAsync()!;

            Console.WriteLine($"Response Status: {httpResponse?.StatusCode}");
            Console.WriteLine($"Response Content: {httpResponseContent}");

            if (httpResponse?.StatusCode == HttpStatusCode.OK)
                response = CommonHelper.DeserializeObject<GetUserByEmailResponse>(httpResponseContent);
            else

            return response;
        }

        public Task GetUserByEmail()
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticateResponse> Authenticate(string moodleUrl, string token, string email)
        {
            try
            {
                var userData = await GetUserByEmail(moodleUrl, token, email);
                
                if (userData != null && userData.Any())
                {

                    var userResponse = new GetUserByEmailResponse();
                    userResponse.AddRange(userData);
                    
                    return new AuthenticateResponse
                    {
                        Success = true,
                        Token = token,
                        UserData = userResponse
                    };
                }
                else
                {
                    return new AuthenticateResponse
                    {
                        Success = false,
                        Error = "User not found",
                        Errorcode = "USER_NOT_FOUND"
                    };
                }
            }
            catch (Exception ex)
            {
                return new AuthenticateResponse
                {
                    Success = false,
                    Error = $"Exception: {ex.Message} - StackTrace: {ex.StackTrace}",
                    Errorcode = "Exception"
                };
            }
        }


    }
}
