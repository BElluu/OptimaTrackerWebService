using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OptimaTrackerWebService.Models;

namespace OptimaTrackerWebService.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration configuration;
        public DatabaseContext(IConfiguration config)
        {
            configuration = config;
        }
        public DbSet<Company> companies { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<EventDict> eventsDict { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Company
            modelBuilder.Entity<Company>(c =>
            {
                c.ToTable("Company");
                c.Property(co => co.Id)
                 .ValueGeneratedOnAdd()
                 .UseIdentityColumn()
                 .IsRequired();
                c.HasIndex(co => co.SerialKey)
                .IsUnique();
                c.HasMany(e => e.Events)
                .WithOne(co => co.Company)
                .IsRequired();
            });

            // Event

            modelBuilder.Entity<Event>(e =>
            {
                e.ToTable("Event");
                e.HasOne(c => c.Company)
                .WithMany(ev => ev.Events)
                .HasForeignKey(c => c.CompanyId);
                e.Property(ev => ev.ProcedureIdentity).HasColumnName("ProcedureId");
                e.Ignore(ev => ev.ProcedureId);
            });

            // Events Dict

            modelBuilder.Entity<EventDict>(d =>
            {
                d.ToTable("EventsDict");
                d.HasData(
                    new EventDict { ProcedureId = "Logowanie", ProcedureDescription = "Okno logowania" },
                    new EventDict { ProcedureId = "BazLista", ProcedureDescription = "Okno listy baz danych" },
                    new EventDict { ProcedureId = "KreatorBazy", ProcedureDescription = "Wizard bazy danych" },
                    new EventDict { ProcedureId = "CfgStanowiskoOgolneParametry", ProcedureDescription = "Ustawienia ogolne stanowiska" },
                    new EventDict { ProcedureId = "CfgSerwisOperacjiAutomatycznych", ProcedureDescription = "Ustawienia Serwisu Operacji Automatycznych" });
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration["ConnectionStrings:OptimaTrackerAppConnection"]);
        }

    }
}
