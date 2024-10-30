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
    public class ContactPersonService : IContactPersonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactPersonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ContactPersonEntity> GetByIdAsync(int contactPersonId)
        {
            return await _unitOfWork.ContactPersonRepository.GetByIdAsync(contactPersonId);
        }

        public async Task<IEnumerable<ContactPersonEntity>> GetAllAsync()
        {
            return await _unitOfWork.ContactPersonRepository.GetAllAsync();
        }

        public async Task<IEnumerable<MilitaryUnitEntity>> GetMilitaryUnitsAsync(int contactPersonId)
        {
            return await _unitOfWork.ContactPersonRepository.GetMilitaryUnits(contactPersonId);
        }

        public async Task AddAsync(ContactPersonEntity contactPerson)
        {
            ValidateContactPerson(contactPerson);
            await _unitOfWork.ContactPersonRepository.AddAsync(contactPerson);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(ContactPersonEntity contactPerson)
        {
            ValidateContactPerson(contactPerson);
            await _unitOfWork.ContactPersonRepository.UpdateAsync(contactPerson);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int contactPersonId)
        {
            await _unitOfWork.ContactPersonRepository.DeleteAsync(contactPersonId);
            await _unitOfWork.SaveChangesAsync();
        }
        private void ValidateContactPerson(ContactPersonEntity contactPerson)
        {
            var validator = new ContactPersonValidator();
            var result = validator.Validate(contactPerson);

            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
        }
    }
}
