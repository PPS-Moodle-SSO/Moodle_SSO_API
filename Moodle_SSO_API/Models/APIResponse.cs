using System.Net;

namespace Moodle_SSO_API.Models
{
    public class APIResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessful { get; set; } = true;
        public List<string>? ErrorsMessage { get; set; }
        public T? Result { get; set; }
    }
}

