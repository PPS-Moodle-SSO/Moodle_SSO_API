using Moodle_SSO_API.Handlers.IHandler;
using Moodle_SSO_API.Models;
using Moodle_SSO_API.Repository.IRepository;

namespace Moodle_SSO_API.Handlers
{
    public class AccountHandler : IAccountHandler
    {
        public IAccountRepository _accountRepository;

        public AccountHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            IEnumerable<Account> accountList = await _accountRepository.GetAll();

            return await Task.FromResult(accountList);
        }
    }
}
