using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PromotionEventsApp.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Member> Events { get; set; }
    }
}
