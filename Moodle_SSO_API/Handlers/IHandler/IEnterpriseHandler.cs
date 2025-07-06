using Moodle_SSO_API.Handlers.Enterprises.ModelsDto;
using Moodle_SSO_API.Models;

namespace Moodle_SSO_API.Handlers.IHandler
{
    public interface IEnterpriseHandler
    {
        /// <summary>
        /// Retrieves all enterprises.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing a collection of enterprises.</returns>
        Task<IEnumerable<Enterprise>> GetAllEnterprises();

        /// <summary>
        /// Retrieves an enterprise by its ID.
        /// </summary>
        /// <param name="id">The ID of the enterprise to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation, containing the enterprise if found; otherwise, null.</returns>
        Task<Enterprise?> GetEnterpriseById(int id);

        /// <summary>
        /// Creates a new enterprise.
        /// </summary>
        /// <param name="enterprise">The enterprise to create.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task CreateEnterprise(Enterprise enterprise);

        /// <summary>
        /// Retrieves an enterprise by its Domain.
        /// </summary>
        /// <param name="id">The Domain of the enterprise to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation, containing the enterprise if found; otherwise, null.</returns>
        Task<Enterprise?> GetEnterpriseByDomain(string domain);
        Task<Enterprise?> UpdateEnterprise(UpdateEnterpriseRequestDto requestDto);

    }
}
