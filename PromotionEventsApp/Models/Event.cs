using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PromotionEventsApp.Models
{
    public class Event
    {  
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Image { get; set; }
        public virtual ICollection<EventSpot> Spots { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Apartment { get; set; }
    }
}
