using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEventsApp.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string SRC { get; set; }
        public virtual ICollection<EventSpot> Spots { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}
