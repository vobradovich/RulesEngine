using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RulesEngine.Models
{
    public class PolicyContext : DbContext
    {
        public virtual DbSet<Policy> Policies { get; set; }
        public virtual DbSet<PolicyModel> PolicyModels { get; set; }
        public virtual DbSet<PolicyRule> PolicyRules { get; set; }
        public virtual DbSet<PolicyVersion> PolicyVersions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase();
        }
    }
}