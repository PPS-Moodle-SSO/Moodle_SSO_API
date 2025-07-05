using Moodle_SSO_API.Models;

namespace Moodle_SSO_API.Handlers.IHandler
{
    public interface IAccountHandler
    {
        Task<IEnumerable<Account>> GetAllAccounts();
    }
}
