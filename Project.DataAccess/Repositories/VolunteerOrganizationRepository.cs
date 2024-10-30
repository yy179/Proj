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
    public class VolunteerOrganizationRepository : IVolunteerOrganizationRepository
    {
        private readonly ProjectDbContext _context;

        public VolunteerOrganizationRepository(ProjectDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrganizationEntity>> GetOrganizationsForVolunteer(int volunteerId)
        {
            return await _context.VolunteerOrganizations
                .Where(vo => vo.VolunteerId == volunteerId)
                .Select(vo => vo.Organization)
                .ToListAsync();
        }
        public async Task<List<VolunteerEntity>> GetVolunteersForOrganization(int organizationId)
        {
            return await _context.VolunteerOrganizations
                .Where(vo => vo.OrganizationId == organizationId)
                .Select(vo => vo.Volunteer)
                .ToListAsync();
        }
        public async Task AddVolunteerToOrganization(int volunteerId, int organizationId)
        {
            var volunteerOrganization = new VolunteerOrganizationEntity
            {
                VolunteerId = volunteerId,
                OrganizationId = organizationId
            };

            _context.VolunteerOrganizations.Add(volunteerOrganization);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveVolunteerFromOrganization(int volunteerId, int organizationId)
        {
            var volunteerOrganization = await _context.VolunteerOrganizations
                .FirstOrDefaultAsync(vo => vo.VolunteerId == volunteerId && vo.OrganizationId == organizationId);

            if (volunteerOrganization != null)
            {
                _context.VolunteerOrganizations.Remove(volunteerOrganization);
                await _context.SaveChangesAsync();
            }
        }
    }
}
