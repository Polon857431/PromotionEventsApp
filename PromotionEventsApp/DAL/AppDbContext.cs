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

        public DbSet<Event> Events { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Spot> Spots { get; set; }
        public DbSet<EventSpot> EventSpots { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            #region Event

            builder.Entity<Event>().HasKey(_ => _.Id);
            builder.Entity<Event>().Property(_ => _.Name).HasMaxLength(255).IsRequired(true);
            builder.Entity<Event>().Property(_ => _.StartTime).IsRequired(true);
            builder.Entity<Event>().Property(_ => _.EndTime).IsRequired(true);

            #endregion


            #region Spot

            builder.Entity<Spot>().HasKey(_ => _.Id);
            builder.Entity<Spot>().Property(_ => _.Name).HasMaxLength(255).IsRequired(true);


            #endregion

            #region ManyToManyRelationships Event - User

            builder.Entity<Member>().HasKey(_ => new { _.EventId, _.UserId });

            builder.Entity<Member>()
                .HasOne<Event>(_ => _.Event)
                .WithMany(e => e.Members)
                .HasForeignKey(_ => _.EventId).IsRequired(true);

            builder.Entity<Member>()
                .HasOne<User>(_ => _.User)
                .WithMany(u => u.Events)
                .HasForeignKey(_ => _.UserId).IsRequired(true);
            #endregion

            #region ManyToManyRelationships Event - User

            builder.Entity<EventSpot>().HasKey(_ => new { _.EventId, _.SpotId });

            builder.Entity<EventSpot>()
                .HasOne<Event>(_ => _.Event)
                .WithMany(e => e.Spots)
                .HasForeignKey(_ => _.EventId).IsRequired(true);

            builder.Entity<EventSpot>()
                .HasOne<Spot>(_ => _.Spot)
                .WithMany(s => s.Events)
                .HasForeignKey(_ => _.SpotId).IsRequired(true);
            #endregion




        }


    }
}
