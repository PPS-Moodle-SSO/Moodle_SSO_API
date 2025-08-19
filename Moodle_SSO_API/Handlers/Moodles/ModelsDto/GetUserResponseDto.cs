namespace Moodle_SSO_API.Handlers.Moodles.Models
{
    public class GetUserResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
    }
}