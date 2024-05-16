using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Core.Entities;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configurations;

        public DbSet<Template> Templates { get; set; }
        public DbSet<Information> Informations { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<CandidateProfile> CandidateProfiles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options,
            IConfiguration configurations) : base(options)
        {
            _configurations = configurations;
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_configurations["DB"] == "COSMO")
            {
                optionsBuilder.UseCosmos(_configurations["Cosmos:URI"] ?? "", _configurations["Cosmos:PrimaryKey"] ?? "", _configurations["Cosmos:Database"] ?? "");              
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Template>()
                .ToContainer("EmployerTemplate")
                .HasPartitionKey(a => a.Id);
            builder.Entity<Information>()
              .ToContainer("Information")
              .HasPartitionKey(a => a.Id);
            builder.Entity<CandidateProfile>()
             .ToContainer("CandidateProfile")
             .HasPartitionKey(a => a.Id);
          
            builder.Entity<Information>().OwnsMany(a => a.Questions);          
            builder.Entity<CandidateProfile>().OwnsMany(a => a.Questions);          
        }   

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }

      

      
    }
}