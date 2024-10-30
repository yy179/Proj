using Project.Abstractions.Services;
using Project.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Services
{
    public class MilitaryUnitContactPersonService : IMilitaryUnitContactPersonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MilitaryUnitContactPersonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddContactPersonToMilitaryUnit(int contactPersonId, int militaryUnitId)
        {
            await _unitOfWork.MilitaryUnitContactPersonRepository.AddContactPersonToMilitaryUnit(contactPersonId, militaryUnitId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RemoveContactPersonFromMilitaryUnit(int contactPersonId, int militaryUnitId)
        {
            await _unitOfWork.MilitaryUnitContactPersonRepository.RemoveContactPersonFromMilitaryUnit(contactPersonId, militaryUnitId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
