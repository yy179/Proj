using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    public class OrganizationEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Description { get; set; }

        public ICollection<RequestEntity>? CompletedRequests { get; set; }
        public ICollection<RequestEntity>? ActiveRequests { get; set; }
        public ICollection<VolunteerOrganizationEntity>? VolunteerOrganizations { get; set; }

    }
}
