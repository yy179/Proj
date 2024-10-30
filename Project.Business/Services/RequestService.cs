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
    public class RequestService : IRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestEntity> GetByIdAsync(int requestId)
        {
            return await _unitOfWork.RequestRepository.GetByIdAsync(requestId);
        }

        public async Task<IEnumerable<RequestEntity>> GetAllAsync()
        {
            return await _unitOfWork.RequestRepository.GetAllAsync();
        }

        public async Task<IEnumerable<RequestEntity>> GetByStatusAsync(bool isActive)
        {
            return await _unitOfWork.RequestRepository.GetByStatusAsync(isActive);
        }

        public async Task AddAsync(RequestEntity request)
        {
            ValidateRequest(request);
            await _unitOfWork.RequestRepository.AddAsync(request);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(RequestEntity request)
        {
            ValidateRequest(request);
            await _unitOfWork.RequestRepository.UpdateAsync(request);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int requestId)
        {
            await _unitOfWork.RequestRepository.DeleteAsync(requestId);
            await _unitOfWork.SaveChangesAsync();
        }
        private void ValidateRequest(RequestEntity request)
        {
            var validator = new RequestValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
        }
    }
}
