using Moodle_SSO_API.Services.HTTP.Models;

namespace Moodle_SSO_API.Services.Interfaces
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> SendGetRequest(string requestUrl, IDictionary<string, string>? requestHeader = null, int retries = 1);
        Task<HttpResponseMessage> SendPostRequest(string requestUrl, object requestBody, IDictionary<string, string>? requestHeader = null, HttpBodyTypeEnum bodyType = HttpBodyTypeEnum.Json);
        Task<HttpResponseMessage> SendPutRequest(string requestUrl, object requestBody, IDictionary<string, string>? requestHeader = null);
        Task<HttpResponseMessage> SendRequest(HttpMethod httpMethod, string requestUrl, object requestBody, IDictionary<string, string>? requestHeader = null, HttpBodyTypeEnum bodyType = HttpBodyTypeEnum.Json);
    }
}