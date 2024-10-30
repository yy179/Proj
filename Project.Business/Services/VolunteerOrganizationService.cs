using FluentValidation;
using Project.Abstractions.Services;
using Project.Business.Validators;
using Project.DataAccess;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Services
{
    public class VolunteerOrganizationService : IVolunteerOrganizationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VolunteerOrganizationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<OrganizationEntity>> GetOrganizationsForVolunteerAsync(int volunteerId)
        {
            return await _unitOfWork.VolunteerOrganizationRepository.GetOrganizationsForVolunteer(volunteerId);
        }

        public async Task<List<VolunteerEntity>> GetVolunteersForOrganizationAsync(int organizationId)
        {
            return await _unitOfWork.VolunteerOrganizationRepository.GetVolunteersForOrganization(organizationId);
        }

        public async Task AddVolunteerToOrganizationAsync(int volunteerId, int organizationId)
        {
            var existingVolunteers = await _unitOfWork.VolunteerOrganizationRepository.GetVolunteersForOrganization(organizationId);
            if (existingVolunteers.Any(v => v.Id == volunteerId))
            {
                throw new InvalidOperationException("Volunteer is already part of the organization.");
            }
            await _unitOfWork.VolunteerOrganizationRepository.AddVolunteerToOrganization(volunteerId, organizationId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RemoveVolunteerFromOrganizationAsync(int volunteerId, int organizationId)
        {
            var existingVolunteers = await _unitOfWork.VolunteerOrganizationRepository.GetVolunteersForOrganization(organizationId);
            if (!existingVolunteers.Any(v => v.Id == volunteerId))
            {
                throw new ArgumentException("Volunteer is not part of the organization.");
            }
            await _unitOfWork.VolunteerOrganizationRepository.RemoveVolunteerFromOrganization(volunteerId, organizationId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
