using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Abstractions.Services
{
    public interface IVolunteerOrganizationService
    {
        Task<List<OrganizationEntity>> GetOrganizationsForVolunteerAsync(int volunteerId);
        Task<List<VolunteerEntity>> GetVolunteersForOrganizationAsync(int organizationId);
        Task AddVolunteerToOrganizationAsync(int volunteerId, int organizationId);
        Task RemoveVolunteerFromOrganizationAsync(int volunteerId, int organizationId);
    }
}
