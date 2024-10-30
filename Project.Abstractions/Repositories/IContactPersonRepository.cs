using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Abstractions.Repositories
{
    public interface IContactPersonRepository
    {
        Task<IEnumerable<ContactPersonEntity>> GetAllAsync();
        Task<ContactPersonEntity> GetByIdAsync(int contactPersonId);
        Task<IEnumerable<MilitaryUnitEntity>> GetMilitaryUnits(int contactPersonId);
        Task AddAsync(ContactPersonEntity contactPerson);
        Task UpdateAsync(ContactPersonEntity contactPerson);
        Task DeleteAsync(int contactPersonId);
    }
}
