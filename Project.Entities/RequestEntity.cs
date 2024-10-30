using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    public class RequestEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public int Importance { get; set; }
        public bool IsActive { get; set; }
        public int? CompletedByVolunteerId { get; set; }
        public VolunteerEntity CompletedByVolunteer { get; set; }

        public int? TakenByVolunteerId { get; set; }
        public VolunteerEntity TakenByVolunteer { get; set; }

        public int? OrganizationCompletedById { get; set; }
        public OrganizationEntity OrganizationCompletedBy { get; set; }

        public int? OrganizationTakenById { get; set; }
        public OrganizationEntity OrganizationTakenBy { get; set; }

        public int MilitaryUnitId { get; set; }
        public MilitaryUnitEntity MilitaryUnit { get; set; }
    }
}
