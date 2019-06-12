using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PromotionEventsApp.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public ICollection<Member> Events { get; set; }
        public ICollection<VisitedSpot> Spots { get; set; }
    }
}
