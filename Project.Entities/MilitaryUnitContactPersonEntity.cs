using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    public class MilitaryUnitContactPersonEntity
    {
        public int MilitaryUnitId { get; set; }
        public MilitaryUnitEntity MilitaryUnit { get; set; }

        public int ContactPersonId { get; set; }
        public ContactPersonEntity ContactPerson { get; set; }
    }
}
