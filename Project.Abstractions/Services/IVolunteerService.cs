using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Abstractions.Services
{
    public interface IVolunteerService
    {
        Task<IEnumerable<VolunteerEntity>> GetAllAsync();
        Task<VolunteerEntity> GetByIdAsync(int volunteerId);
        Task<List<RequestEntity>> GetCompletedRequestsAsync(int volunteerId);
        Task<List<RequestEntity>> GetActiveRequestsAsync(int volunteerId);
        Task<List<OrganizationEntity>> GetOrganizationsAsync(int volunteerId);
        Task AddAsync(VolunteerEntity volunteer);
        Task UpdateAsync(VolunteerEntity volunteer);
        Task DeleteAsync(int volunteerId);
    }
}
