using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    public class VolunteerOrganizationEntity
    {
        public int VolunteerId { get; set; }
        public VolunteerEntity Volunteer { get; set; }
        public int OrganizationId { get; set; }
        public OrganizationEntity Organization { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}
