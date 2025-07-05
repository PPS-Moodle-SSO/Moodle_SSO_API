using Moodle_SSO_API.Data;
using Moodle_SSO_API.Models;
using Moodle_SSO_API.Repository.IRepository;

namespace Moodle_SSO_API.Repository
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        private readonly AppDbContext _db;
        public AccountRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Account> Update(Account entity)
        {
            //para crear la operecion en la clase de la operacion
            //entity.date = DateTime.Now;
            _db.account.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
