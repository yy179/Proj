using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Abstractions.Services
{
    public interface IMilitaryUnitService
    {
        Task<MilitaryUnitEntity> GetByIdAsync(int militaryUnitId);
        Task<IEnumerable<MilitaryUnitEntity>> GetAllAsync();
        Task<IEnumerable<RequestEntity>> GetActiveRequestsAsync(int militaryUnitId);
        Task<IEnumerable<RequestEntity>> GetCompletedRequestsAsync(int militaryUnitId);
        Task<IEnumerable<ContactPersonEntity>> GetContactPersonsAsync(int militaryUnitId);
        Task AddAsync(MilitaryUnitEntity militaryUnit);
        Task UpdateAsync(MilitaryUnitEntity militaryUnit);
        Task DeleteAsync(int militaryUnitId);
    }
}
