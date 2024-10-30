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
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly ProjectDbContext _context;

        public VolunteerRepository(ProjectDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VolunteerEntity>> GetAllAsync()
        {
            return await _context.Volunteers.ToListAsync();
        }

        public async Task<VolunteerEntity> GetByIdAsync(int volunteerId)
        {
            return await _context.Volunteers.FindAsync(volunteerId);
        }
        public async Task<List<RequestEntity>> GetCompletedRequests(int volunteerId)
        {
            return await _context.Volunteers.Where(v => v.Id == volunteerId).SelectMany(v => v.CompletedRequests).ToListAsync();
        }
        public async Task<List<RequestEntity>> GetActiveRequests(int volunteerId)
        {
            return await _context.Volunteers.Where(v => v.Id == volunteerId).SelectMany(v => v.ActiveRequests).ToListAsync();
        }
        public async Task<List<OrganizationEntity>> GetOrganizations(int volunteerId)
        {
            var volunteer = await _context.Volunteers
                .Include(v => v.VolunteerOrganizations)
                    .ThenInclude(vo => vo.Organization)
                .FirstOrDefaultAsync(v => v.Id == volunteerId);

            return volunteer?.VolunteerOrganizations.Select(vo => vo.Organization).ToList();
        }

        public async Task AddAsync(VolunteerEntity volunteer)
        {
            await _context.Volunteers.AddAsync(volunteer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(VolunteerEntity volunteer)
        {
            _context.Volunteers.Update(volunteer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int volunteerId)
        {
            var volunteer = await _context.Volunteers.FindAsync(volunteerId);
            if (volunteer != null)
            {
                _context.Volunteers.Remove(volunteer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
