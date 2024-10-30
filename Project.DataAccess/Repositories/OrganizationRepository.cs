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
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ProjectDbContext _context;

        public OrganizationRepository(ProjectDbContext context)
        {
            _context = context;
        }

        public async Task<OrganizationEntity> GetByIdAsync(int organizationId)
        {
            return await _context.Organizations.FindAsync(organizationId);
        }

        public async Task<IEnumerable<OrganizationEntity>> GetAllAsync()
        {
            return await _context.Organizations.ToListAsync();
        }
        public async Task<List<RequestEntity>> GetCompletedRequests(int organizationId)
        {
            return await _context.Organizations.Where(v => v.Id == organizationId).SelectMany(v => v.CompletedRequests).ToListAsync();
        }
        public async Task<List<RequestEntity>> GetActiveRequests(int organizationId)
        {
            return await _context.Organizations.Where(v => v.Id == organizationId).SelectMany(v => v.ActiveRequests).ToListAsync();
        }
        public async Task AddAsync(OrganizationEntity organization)
        {
            await _context.Organizations.AddAsync(organization);
            await _context.SaveChangesAsync();
        }
        public async Task<List<VolunteerEntity>> GetVolunteers(int organizationId)
        {
            var organization = await _context.Organizations
                .Include(o => o.VolunteerOrganizations)
                    .ThenInclude(vo => vo.Volunteer)
                .FirstOrDefaultAsync(o => o.Id == organizationId);

            return organization?.VolunteerOrganizations.Select(vo => vo.Volunteer).ToList();
        }


        public async Task UpdateAsync(OrganizationEntity organization)
        {
            _context.Organizations.Update(organization);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int organizationId)
        {
            var organization = await _context.Organizations.FindAsync(organizationId);
            if (organization != null)
            {
                _context.Organizations.Remove(organization);
                await _context.SaveChangesAsync();
            }
        }
    }
}
