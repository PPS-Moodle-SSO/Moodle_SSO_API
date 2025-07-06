namespace Moodle_SSO_API.Services.Moodle.Models
{
    public class GetUserByEmailRequest
    {
        public string WSToken { get; set; } = string.Empty;
        public string WSFunction { get; set; } = "core_user_get_users_by_field";
        public string MoodleWSRequestFormat { get; set; } = "json";
        public string Field { get; set; } = "email";
        public string Value { get; set; } = string.Empty;
    }
}
