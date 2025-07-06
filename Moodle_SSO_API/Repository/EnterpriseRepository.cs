using Moodle_SSO_API.Data;
using Moodle_SSO_API.Models;
using Moodle_SSO_API.Repository.IRepository;

namespace Moodle_SSO_API.Repository
{
    public class EnterpriseRepository : Repository<Enterprise>, IEnterpriseRepository
    {
        private readonly AppDbContext _db;
        public EnterpriseRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task Update(Enterprise entity)
        {
            _db.Enterprise.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}
