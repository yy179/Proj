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
    public class MilitaryUnitContactPersonRepository : IMilitaryUnitContactPersonRepository
    {
        private readonly ProjectDbContext _context;

        public MilitaryUnitContactPersonRepository(ProjectDbContext context)
        {
            _context = context;
        }
        public async Task AddContactPersonToMilitaryUnit(int contactPersonId, int militaryUnitId)
        {
            var militaryUnitContactPerson = new MilitaryUnitContactPersonEntity
            {
                ContactPersonId = contactPersonId,
                MilitaryUnitId = militaryUnitId
            };

            _context.MilitaryUnitContactPersons.Add(militaryUnitContactPerson);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveContactPersonFromMilitaryUnit(int contactPersonId, int militaryUnitId)
        {
            var militaryUnitContactPerson = await _context.MilitaryUnitContactPersons
                .FirstOrDefaultAsync(vo => vo.ContactPersonId == contactPersonId && vo.MilitaryUnitId == militaryUnitId);

            if (militaryUnitContactPerson != null)
            {
                _context.MilitaryUnitContactPersons.Remove(militaryUnitContactPerson);
                await _context.SaveChangesAsync();
            }
        }
    }
}
