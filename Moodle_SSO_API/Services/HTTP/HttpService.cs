using Moodle_SSO_API.Helpers;
using Moodle_SSO_API.Services.HTTP.Models;
using Moodle_SSO_API.Services.Interfaces;
using System.Text;

namespace Moodle_SSO_API.Services.HTTP
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<HttpResponseMessage> SendGetRequest(
            string url,
            IDictionary<string, string> requestHeader,
            int retries
        )
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

            if (requestHeader != null)
                foreach (var header in requestHeader)
                    httpRequest.Headers.Add(header.Key, header.Value);

            string errorMessage = string.Empty;

            if (retries <= 10)
            {
                try
                {
                    HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest);

                    return await Task.FromResult(httpResponse);
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    retries += 1;

                    _ = await ((IHttpService)this).SendGetRequest(url, requestHeader, retries);
                }
            }

            throw new HttpRequestException(errorMessage);
        }

        public async Task<HttpResponseMessage> SendPostRequest(
            string requestUrl,
            object requestBody,
            IDictionary<string, string> requestHeader,
            HttpBodyTypeEnum bodyType
        )
        {
            try
            {
                HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);

                if (requestHeader != null)
                    foreach (var header in requestHeader)
                        httpRequest.Headers.Add(header.Key, header.Value);

                switch (bodyType)
                {
                    case HttpBodyTypeEnum.Json:
                        httpRequest.Content = new StringContent(CommonHelper.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                        break;

                    case HttpBodyTypeEnum.UrlEncoded:
                        httpRequest.Content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)requestBody);
                        break;
                }

                HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest);

                return await Task.FromResult(httpResponse);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> SendPutRequest(string requestUrl, object requestBody, IDictionary<string, string>? requestHeader = null)
        {
            try
            {
                HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Put, requestUrl);

                if (requestHeader != null)
                    foreach (var header in requestHeader)
                        httpRequest.Headers.Add(header.Key, header.Value);

                httpRequest.Content = new StringContent(CommonHelper.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest);

                return await Task.FromResult(httpResponse);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> SendRequest(
            HttpMethod httpMethod,
            string requestUrl,
            object requestBody,
            IDictionary<string, string> requestHeader,
            HttpBodyTypeEnum bodyType
        )
        {
            try
            {
                HttpRequestMessage httpRequest = new HttpRequestMessage(httpMethod, requestUrl);

                if (requestHeader != null)
                    foreach (var header in requestHeader)
                        httpRequest.Headers.Add(header.Key, header.Value);

                switch (bodyType)
                {
                    case HttpBodyTypeEnum.Json:
                        httpRequest.Content = new StringContent(CommonHelper.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                        break;

                    case HttpBodyTypeEnum.UrlEncoded:
                        httpRequest.Content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)requestBody);
                        break;
                }

                HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest);

                return await Task.FromResult(httpResponse);
            }
            catch
            {
                throw;
            }
        }
    }
}