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
    public class VolunteerService : IVolunteerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VolunteerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<VolunteerEntity>> GetAllAsync()
        {
            return await _unitOfWork.VolunteerRepository.GetAllAsync();
        }

        public async Task<VolunteerEntity> GetByIdAsync(int volunteerId)
        {
            return await _unitOfWork.VolunteerRepository.GetByIdAsync(volunteerId);
        }

        public async Task<List<RequestEntity>> GetCompletedRequestsAsync(int volunteerId)
        {
            return await _unitOfWork.VolunteerRepository.GetCompletedRequests(volunteerId);
        }

        public async Task<List<RequestEntity>> GetActiveRequestsAsync(int volunteerId)
        {
            return await _unitOfWork.VolunteerRepository.GetActiveRequests(volunteerId);
        }

        public async Task<List<OrganizationEntity>> GetOrganizationsAsync(int volunteerId)
        {
            return await _unitOfWork.VolunteerRepository.GetOrganizations(volunteerId);
        }

        public async Task AddAsync(VolunteerEntity volunteer)
        {
            ValidateVolunteer(volunteer);
            await _unitOfWork.VolunteerRepository.AddAsync(volunteer);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(VolunteerEntity volunteer)
        {
            ValidateVolunteer(volunteer);
            await _unitOfWork.VolunteerRepository.UpdateAsync(volunteer);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int volunteerId)
        {
            await _unitOfWork.VolunteerRepository.DeleteAsync(volunteerId);
            await _unitOfWork.SaveChangesAsync();
        }
        private void ValidateVolunteer(VolunteerEntity volunteer)
        {
            var validator = new VolunteerValidator();
            var result = validator.Validate(volunteer);

            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
        }
    }
}
