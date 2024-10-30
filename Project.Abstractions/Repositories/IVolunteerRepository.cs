using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Abstractions.Repositories
{
    public interface IVolunteerRepository
    {
        Task<IEnumerable<VolunteerEntity>> GetAllAsync();
        Task<VolunteerEntity> GetByIdAsync(int id);
        Task<List<RequestEntity>> GetCompletedRequests(int volunteerId);
        Task<List<RequestEntity>> GetActiveRequests(int volunteerId);
        Task<List<OrganizationEntity>> GetOrganizations(int volunteerId);
        Task AddAsync(VolunteerEntity volunteer);
        Task UpdateAsync(VolunteerEntity volunteer);
        Task DeleteAsync(int id);
    }
}
