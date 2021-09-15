using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RulesEngine.Models
{
    public class PolicyContext : DbContext
    {
        public virtual DbSet<PolicyModel> PolicyModels { get; set; }
        public virtual DbSet<Policy> Policies { get; set; }
        public virtual DbSet<PolicyVersion> PolicyVersions { get; set; }
        public virtual DbSet<PolicyRule> PolicyRules { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("RulesEngine");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PolicyModel>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Policy>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<PolicyVersion>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<PolicyRule>().HasKey(c => new { c.PolicyVersionId, c.Order });
        }

    }
}