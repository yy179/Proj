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
    public class MilitaryUnitRepository : IMilitaryUnitRepository
    {
        private readonly ProjectDbContext _context;

        public MilitaryUnitRepository(ProjectDbContext context)
        {
            _context = context;
        }

        public async Task<MilitaryUnitEntity> GetByIdAsync(int militaryUnitId)
        {
            return await _context.MilitaryUnits.FindAsync(militaryUnitId);
        }

        public async Task<IEnumerable<MilitaryUnitEntity>> GetAllAsync()
        {
            return await _context.MilitaryUnits.ToListAsync();
        }
        public async Task<IEnumerable<RequestEntity>> GetActiveRequests(int militaryUnitId)
        {
            var militaryUnit = await _context.MilitaryUnits
                .Include(mu => mu.ActiveRequests)
                .FirstOrDefaultAsync(mu => mu.Id == militaryUnitId);

            return militaryUnit?.ActiveRequests;
        }

        public async Task<IEnumerable<RequestEntity>> GetCompletedRequests(int militaryUnitId)
        {
            var militaryUnit = await _context.MilitaryUnits
                .Include(mu => mu.CompletedRequests)
                .FirstOrDefaultAsync(mu => mu.Id == militaryUnitId);

            return militaryUnit?.CompletedRequests;
        }


        public async Task<IEnumerable<ContactPersonEntity>> GetContactPersons(int militaryUnitId)
        {
            var militaryUnit = await _context.MilitaryUnits
                .Include(mu => mu.MilitaryUnitContactPersons)
                    .ThenInclude(cr => cr.ContactPerson)
                .FirstOrDefaultAsync(mu => mu.Id == militaryUnitId);

            return militaryUnit?.MilitaryUnitContactPersons.Select(cr => cr.ContactPerson).ToList();
        }

        public async Task AddAsync(MilitaryUnitEntity militaryUnit)
        {
            await _context.MilitaryUnits.AddAsync(militaryUnit);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MilitaryUnitEntity militaryUnit)
        {
            _context.MilitaryUnits.Update(militaryUnit);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int militaryUnitId)
        {
            var militaryUnit = await _context.MilitaryUnits.FindAsync(militaryUnitId);
            if (militaryUnit != null)
            {
                _context.MilitaryUnits.Remove(militaryUnit);
                await _context.SaveChangesAsync();
            }
        }
    }
}
