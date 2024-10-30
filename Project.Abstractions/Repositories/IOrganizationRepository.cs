using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Abstractions.Repositories
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<OrganizationEntity>> GetAllAsync();
        Task<OrganizationEntity> GetByIdAsync(int organizationId);
        Task<List<VolunteerEntity>> GetVolunteers(int organizationId);
        Task<List<RequestEntity>> GetCompletedRequests(int organizationId);
        Task<List<RequestEntity>> GetActiveRequests(int organizationId);
        Task AddAsync(OrganizationEntity organization);
        Task UpdateAsync(OrganizationEntity organization);
        Task DeleteAsync(int id);
    }
}
