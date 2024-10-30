using Microsoft.EntityFrameworkCore;
using Project.Abstractions.Repositories;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccess.Repositories
{
    public class ContactPersonRepository : IContactPersonRepository
    {
        private readonly ProjectDbContext _context;

        public ContactPersonRepository(ProjectDbContext context)
        {
            _context = context;
        }

        public async Task<ContactPersonEntity> GetByIdAsync(int contactPersonId)
        {
            return await _context.ContactPersons.FindAsync(contactPersonId);
        }

        public async Task<IEnumerable<ContactPersonEntity>> GetAllAsync()
        {
            return await _context.ContactPersons.ToListAsync();
        }
        public async Task<IEnumerable<MilitaryUnitEntity>> GetMilitaryUnits(int contactPersonId)
        {
            var contactPerson = await _context.ContactPersons
                .Include(mu => mu.MilitaryUnitContactPersons)
                    .ThenInclude(mu => mu.MilitaryUnit)
                .FirstOrDefaultAsync(cr => cr.Id == contactPersonId);

            return contactPerson?.MilitaryUnitContactPersons.Select(mu => mu.MilitaryUnit).ToList();
        }
        public async Task AddAsync(ContactPersonEntity contactPerson)
        {
            await _context.ContactPersons.AddAsync(contactPerson);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ContactPersonEntity contactPerson)
        {
            _context.ContactPersons.Update(contactPerson);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int contactPersonId)
        {
            var contactPerson = await _context.ContactPersons.FindAsync(contactPersonId);
            if (contactPerson != null)
            {
                _context.ContactPersons.Remove(contactPerson);
                await _context.SaveChangesAsync();
            }
        }
    }
}
