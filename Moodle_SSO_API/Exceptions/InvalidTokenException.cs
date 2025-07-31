namespace Moodle_SSO_API.Exceptions
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException(string token)
            : base($"El token proporcionado no es válido: {token}") { }
    }
}