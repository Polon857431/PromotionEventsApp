using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PromotionEventsApp.Models;

namespace PromotionEventsApp.DAL
{
    public class AppDbContext : IdentityDbContext<User, Role, int>

    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        private DbSet<Event> Events { get; set; }
    }
}
