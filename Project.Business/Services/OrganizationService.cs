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
    public class OrganizationService : IOrganizationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrganizationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OrganizationEntity> GetByIdAsync(int organizationId)
        {
            return await _unitOfWork.OrganizationRepository.GetByIdAsync(organizationId);
        }

        public async Task<IEnumerable<OrganizationEntity>> GetAllAsync()
        {
            return await _unitOfWork.OrganizationRepository.GetAllAsync();
        }

        public async Task<List<RequestEntity>> GetCompletedRequestsAsync(int organizationId)
        {
            return await _unitOfWork.OrganizationRepository.GetCompletedRequests(organizationId);
        }

        public async Task<List<RequestEntity>> GetActiveRequestsAsync(int organizationId)
        {
            return await _unitOfWork.OrganizationRepository.GetActiveRequests(organizationId);
        }

        public async Task<List<VolunteerEntity>> GetVolunteersAsync(int organizationId)
        {
            return await _unitOfWork.OrganizationRepository.GetVolunteers(organizationId);
        }

        public async Task AddAsync(OrganizationEntity organization)
        {
            ValidateOrganization(organization);
            await _unitOfWork.OrganizationRepository.AddAsync(organization);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrganizationEntity organization)
        {
            ValidateOrganization(organization);
            await _unitOfWork.OrganizationRepository.UpdateAsync(organization);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int organizationId)
        {
            await _unitOfWork.OrganizationRepository.DeleteAsync(organizationId);
            await _unitOfWork.SaveChangesAsync();
        }
        private void ValidateOrganization(OrganizationEntity organization)
        {
            var validator = new OrganizationValidator();
            var result = validator.Validate(organization);

            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
        }
    }
}
