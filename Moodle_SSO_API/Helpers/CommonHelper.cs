using Newtonsoft.Json;

namespace Moodle_SSO_API.Helpers
{
    public class CommonHelper
    {
        public static string SerializeObject(object @object)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return JsonConvert.SerializeObject(@object, settings);
        }

        public static T DeserializeObject<T>(string @string)
            => JsonConvert.DeserializeObject<T>(@string);
    }
}
