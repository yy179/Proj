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
    public class MilitaryUnitService : IMilitaryUnitService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MilitaryUnitService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MilitaryUnitEntity> GetByIdAsync(int militaryUnitId)
        {
            return await _unitOfWork.MilitaryUnitRepository.GetByIdAsync(militaryUnitId);
        }

        public async Task<IEnumerable<MilitaryUnitEntity>> GetAllAsync()
        {
            return await _unitOfWork.MilitaryUnitRepository.GetAllAsync();
        }

        public async Task<IEnumerable<RequestEntity>> GetActiveRequestsAsync(int militaryUnitId)
        {
            return await _unitOfWork.MilitaryUnitRepository.GetActiveRequests(militaryUnitId);
        }

        public async Task<IEnumerable<RequestEntity>> GetCompletedRequestsAsync(int militaryUnitId)
        {
            return await _unitOfWork.MilitaryUnitRepository.GetCompletedRequests(militaryUnitId);
        }

        public async Task<IEnumerable<ContactPersonEntity>> GetContactPersonsAsync(int militaryUnitId)
        {
            return await _unitOfWork.MilitaryUnitRepository.GetContactPersons(militaryUnitId);
        }

        public async Task AddAsync(MilitaryUnitEntity militaryUnit)
        {
            ValidateMilitaryUnit(militaryUnit);
            await _unitOfWork.MilitaryUnitRepository.AddAsync(militaryUnit);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(MilitaryUnitEntity militaryUnit)
        {
            ValidateMilitaryUnit(militaryUnit);
            await _unitOfWork.MilitaryUnitRepository.UpdateAsync(militaryUnit);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int militaryUnitId)
        {
            await _unitOfWork.MilitaryUnitRepository.DeleteAsync(militaryUnitId);
            await _unitOfWork.SaveChangesAsync();
        }
        private void ValidateMilitaryUnit(MilitaryUnitEntity militaryUnit)
        {
            var validator = new MilitaryUnitValidator();
            var result = validator.Validate(militaryUnit);
            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
        }
    }
}
