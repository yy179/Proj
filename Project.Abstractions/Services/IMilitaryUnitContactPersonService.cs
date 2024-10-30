using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Abstractions.Services
{
    public interface IMilitaryUnitContactPersonService
    {
        Task AddContactPersonToMilitaryUnit(int contactPersonId, int militaryUnitId);
        Task RemoveContactPersonFromMilitaryUnit(int contactPersonId, int militaryUnitId);
    }
}
