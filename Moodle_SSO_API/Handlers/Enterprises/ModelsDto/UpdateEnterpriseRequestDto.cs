namespace Moodle_SSO_API.Handlers.Enterprises.ModelsDto
{
    public class UpdateEnterpriseRequestDto
    {
        public int IdEnterprise { get; set; }
        public string MoodleUrl { get; set; }
        public string MoodleApiKey { get; set; }
    }
}
