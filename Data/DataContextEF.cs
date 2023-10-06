using HelloWorld.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HelloWorld.Data
{
    public class DataContextEF : DbContext
    {
        private readonly string _connectionString;
        public DataContextEF(IConfiguration configuration)
        {
            // _config = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }


        public DbSet<Computer>? Computer { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString, optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // set default schema
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            // we don't use .ToTable since we are setting a default schema above and adding our model below .Entity<Computer>
            modelBuilder.Entity<Computer>()
            .HasKey((computer) => computer.ComputerId)
            // .HasNoKey()
            // (name of the table, schema where the table is under(where we look for our Computer table))
            // .ToTable("Computer", "TutorialAppSchema")
            ;
        }

    }
}