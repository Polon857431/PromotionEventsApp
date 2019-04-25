using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PromotionEventsApp.Models;

namespace PromotionEventsApp.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>

    {
        private DbSet<Event> Events { get; set; }
    }
}
