using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    public class VolunteerEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<MessageEntity>? MessagesSent { get; set; }
        public ICollection<MessageEntity>? MessagesReceived { get; set; }
        public ICollection<VolunteerOrganizationEntity>? VolunteerOrganizations { get; set; }
        public ICollection<RequestEntity>? CompletedRequests { get; set; }
        public ICollection<RequestEntity>? ActiveRequests { get; set; }
    }
}
