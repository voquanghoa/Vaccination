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
                Id = 2,
                Username = "admin",
                FullName = "Huu Truong",
                Role = Role.Admin,
                Password = "huu123".Encode(),
                Valid = true
            });
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 3,
                Username = "nurse",
                FullName = "Hoc Le",
                Role = Role.Nurse,
                Password = "hoc123".Encode(),
                Valid = true
            });
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 4,
                Username = "assistant",
                FullName = "Man Nguyen",
                Role = Role.Assistant,
                Password = "man123".Encode(),
                Valid = true
            });
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<RegisteredPatient> RegisteredPatients { get; set; }
    }
}