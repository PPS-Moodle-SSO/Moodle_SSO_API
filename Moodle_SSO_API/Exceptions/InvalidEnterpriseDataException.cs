namespace Moodle_SSO_API.Exceptions
{
    public class InvalidEnterpriseDataException : Exception
    {
        public InvalidEnterpriseDataException(string? message)
            : base(message) { }
    }
}