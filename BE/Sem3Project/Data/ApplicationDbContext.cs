using Microsoft.EntityFrameworkCore;
using Sem3Project.Models;

namespace Sem3Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        public DbSet<VehicleInsurance> VehicleInsurances { get; set; }

        public DbSet<VehiclePolicy> VehiclePolicies { get; set; }

        public DbSet<LifeInsurance> LifeInsurances { get; set; }

        public DbSet<LifePolicy> LifePolicies { get; set; }

        public DbSet<Hospital> Hospitals { get; set; }

        public DbSet<MedicalInsurance> MedicalInsurances { get; set; }

        public DbSet<MedicalPolicy> MedicalPolicies { get; set; }

        public DbSet<HomeInsurance> HomeInsurances { get; set; }

        public DbSet<HomePolicy> HomePolicies { get; set; }
    }
}
