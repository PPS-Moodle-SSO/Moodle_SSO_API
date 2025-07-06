using Moodle_SSO_API.Handlers.Enterprises.ModelsDto;
using Moodle_SSO_API.Handlers.IHandler;
using Moodle_SSO_API.Models;
using Moodle_SSO_API.Repository.IRepository;

namespace Moodle_SSO_API.Handlers.Enterprises
{
    public class EnterpriseHandler : IEnterpriseHandler
    {
        private readonly IEnterpriseRepository _enterpriseRepository;

        public EnterpriseHandler(IEnterpriseRepository enterpriseRepository)
        {
            _enterpriseRepository = enterpriseRepository ?? throw new ArgumentNullException(nameof(enterpriseRepository));
        }

        public Task CreateEnterprise(Enterprise enterprise)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Enterprise>> GetAllEnterprises()
        {
            throw new NotImplementedException();
        }

        public Task<Enterprise?> GetEnterpriseById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Enterprise?> GetEnterpriseByDomain(string domain)
        {
            var enterprise = await _enterpriseRepository.Get(e => e.Domain.Trim() == domain.Trim());
            return enterprise;
        }
        
        public async Task<Enterprise?> UpdateEnterprise(UpdateEnterpriseRequestDto requestDto)
        {
            var enterprise = await _enterpriseRepository.Get(e => e.IdEnterprise == requestDto.IdEnterprise);

            enterprise.MoodleUrl = requestDto.MoodleUrl;
            enterprise.MoodleApiKey = requestDto.MoodleApiKey;
            await _enterpriseRepository.Update(enterprise);

            return enterprise;
        }
    }
}
