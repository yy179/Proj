using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    public class MilitaryUnitEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MilitaryUnitContactPersonEntity>? MilitaryUnitContactPersons { get; set; } = new List<MilitaryUnitContactPersonEntity>();
        public ICollection<RequestEntity> ActiveRequests => Requests.Where(r => r.IsActive).ToList();
        [NotMapped]
        public ICollection<RequestEntity> CompletedRequests => Requests.Where(r => !r.IsActive).ToList();
        [NotMapped]
        public ICollection<RequestEntity> Requests { get; set; } = new List<RequestEntity>();
    }

}
