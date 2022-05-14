using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OptimaTrackerWebService.Models;

namespace OptimaTrackerWebService.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Company> companies { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<EventDetails> eventsDetails { get; set; }
        public DbSet<ProceduresDict> proceduresDict { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Company
            modelBuilder.Entity<Company>(c =>
            {
                c.ToTable("Companies");
                c.Property(co => co.Id).ValueGeneratedOnAdd().UseIdentityColumn().IsRequired().HasColumnName("CompanyId");
                c.HasIndex(co => co.SerialKey).IsUnique();
                c.HasMany(e => e.Events).WithOne(co => co.Company).IsRequired();
            });

            // Event

            modelBuilder.Entity<Event>(e =>
            {
                e.ToTable("Events");
                e.Property(ev => ev.Id).ValueGeneratedOnAdd().UseIdentityColumn().IsRequired().HasColumnName("EventId");
                e.HasIndex("Id");
            });

            // Event Details

            modelBuilder.Entity<EventDetails>(e =>
            {
                e.ToTable("EventsDetails");
                e.Property(ev => ev.Id).ValueGeneratedOnAdd().UseIdentityColumn().IsRequired().HasColumnName("EventId");
                e.HasOne(c => c.Company).WithMany(ev => ev.Events).HasForeignKey(c => c.CompanyId);
                e.Ignore(ev => ev.ProcedureName);
            });

            // Events Dict

            modelBuilder.Entity<ProceduresDict>(d =>
            {
                d.ToTable("ProceduresDict");
                d.Property(d => d.Id).ValueGeneratedOnAdd().UseIdentityColumn().IsRequired().HasColumnName("ProcedureId");
            });
        }
    }
}
