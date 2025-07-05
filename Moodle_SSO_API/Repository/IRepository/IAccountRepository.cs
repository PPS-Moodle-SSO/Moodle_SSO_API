using Moodle_SSO_API.Models;

namespace Moodle_SSO_API.Repository.IRepository
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> Update(Account entity);
    }
}
