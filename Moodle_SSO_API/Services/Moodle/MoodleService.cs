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
        private string WSFunction { get; set; }
        private string MoodleWSRestFormat { get; set; }

        public MoodleService(IConfiguration config, IHttpService httpService)
        {
            _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
            WSFunction = config["Moodle:wsfunction"] ?? throw new ArgumentNullException("WSFunction is not configured.");
            MoodleWSRestFormat = config["Moodle:moodlewsrestformat"] ?? throw new ArgumentNullException("MoodleWSRestFormat is not configured.");
        }

        public async Task<GetUserByEmailResponse> GetUserByEmail(string moodleUrl, string token, string value)
        {
            GetUserByEmailResponse response;

            var url = $"{moodleUrl}";
            var request = new GetUserByEmailRequest
            {
                WSToken = token,
                WSFunction = WSFunction,
                MoodleWSRequestFormat = MoodleWSRestFormat,
                Field = "email",
                Value = value
            };
            var httpResponse = await _httpService.SendPostRequest(url, request, null, bodyType: HttpBodyTypeEnum.Json);
            var httpResponseContent = await httpResponse?.Content?.ReadAsStringAsync()!;

            if (httpResponse?.StatusCode == HttpStatusCode.OK)
                response = CommonHelper.DeserializeObject<GetUserByEmailResponse>(httpResponseContent);
            else
                throw new Exception("User not exists");

            return response;
        }

        public Task GetUserByEmail()
        {
            throw new NotImplementedException();
        }
    }
}
