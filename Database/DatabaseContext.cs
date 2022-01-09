using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OptimaTrackerWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimaTrackerWebService.Database
{
    public class DatabaseContext: DbContext
    {
        private readonly IConfiguration configuration;
        public DatabaseContext(IConfiguration config)
        {
            configuration = config;
        }
        //public DbSet<Track> Track { get; set; }
        public DbSet<Company> companies { get; set; }
        public DbSet<Event> events { get; set; }

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
                c.HasMany(e => e.Events)
                .WithOne(co => co.Company)
                .IsRequired();
            });
            /*                .Property(c => c.Id)
                            .ValueGeneratedOnAdd()
                            .UseIdentityColumn()
                            .IsRequired();*/
            /*            modelBuilder.Entity<Company>()
                            .HasMany(c => c.Events)
                            .WithOne(e => e.Company)
                            .IsRequired();*/

            // Event

            modelBuilder.Entity<Event>(e =>
            {
                e.ToTable("Event");
                e.HasOne(c => c.Company)
                .WithMany(ev => ev.Events)
                .HasForeignKey(c => c.CompanyId);
            });
        }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=OptimaTrackerDb;User Id=OptimaTrackerUser;Password=optima;");
            optionsBuilder.UseNpgsql(configuration["ConnectionStrings:OptimaTrackerAppConnection"]);
        }

    }
}
