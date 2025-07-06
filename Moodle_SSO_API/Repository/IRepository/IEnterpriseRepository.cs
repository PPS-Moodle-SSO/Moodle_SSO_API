using Moodle_SSO_API.Models;

namespace Moodle_SSO_API.Repository.IRepository
{
    public interface IEnterpriseRepository : IRepository<Enterprise>
    {
        Task Update(Enterprise entity);
    }
}
