namespace Moodle_SSO_API.Exceptions
{
    public class EnterpriseNotFoundException : Exception
    {
        public EnterpriseNotFoundException(string? message) : base(message)
        {
        }
    }
}