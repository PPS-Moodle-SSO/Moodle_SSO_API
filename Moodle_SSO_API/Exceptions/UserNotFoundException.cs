namespace Moodle_SSO_API.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string email)
            : base($"No se encontró ningún usuario con el email: {email}") { }
    }
}
