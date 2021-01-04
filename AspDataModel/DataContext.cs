using AspDataModel.Contracts;
using AspDataModel.Models;
using AspDataModel.Utils;
using Microsoft.EntityFrameworkCore;

namespace AspDataModel
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 1,
                Username = "Admin",
                FullName = "Admin",
                Role = Role.Admin,
                Password = "admin123".Encode(),
                Valid = true
            });
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<RegisteredPatient> RegisteredPatients { get; set; }
    }
}