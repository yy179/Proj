using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    public class MessageEntity
    {
        public int Id { get; set; }
        public int FromVolunteerId { get; set; }
        public VolunteerEntity FromVolunteer { get; set; }

        public int ToVolunteerId { get; set; }
        public VolunteerEntity ToVolunteer { get; set; }

        public DateTime SentDate { get; set; }
        public string Text { get; set; }
    }
}
