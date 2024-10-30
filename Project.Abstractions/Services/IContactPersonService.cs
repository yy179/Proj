using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Abstractions.Services
{
    public interface IContactPersonService
    {
        Task<ContactPersonEntity> GetByIdAsync(int contactPersonId);
        Task<IEnumerable<ContactPersonEntity>> GetAllAsync();
        Task<IEnumerable<MilitaryUnitEntity>> GetMilitaryUnitsAsync(int contactPersonId);
        Task AddAsync(ContactPersonEntity contactPerson);
        Task UpdateAsync(ContactPersonEntity contactPerson);
        Task DeleteAsync(int contactPersonId);
    }
}
