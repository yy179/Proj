using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Abstractions.Services
{
    public interface IOrganizationService
    {
        Task<OrganizationEntity> GetByIdAsync(int organizationId);
        Task<IEnumerable<OrganizationEntity>> GetAllAsync();
        Task<List<RequestEntity>> GetCompletedRequestsAsync(int organizationId);
        Task<List<RequestEntity>> GetActiveRequestsAsync(int organizationId);
        Task<List<VolunteerEntity>> GetVolunteersAsync(int organizationId);
        Task AddAsync(OrganizationEntity organization);
        Task UpdateAsync(OrganizationEntity organization);
        Task DeleteAsync(int organizationId);
    }
}
