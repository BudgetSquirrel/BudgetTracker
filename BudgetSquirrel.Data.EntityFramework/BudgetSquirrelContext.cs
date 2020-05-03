using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Data.EntityFramework.Models;
using BudgetSquirrel.Data.EntityFramework.Schema;
using Microsoft.EntityFrameworkCore;

namespace BudgetSquirrel.Data.EntityFramework
{
    public class BudgetSquirrelContext : DbContext
    {
        public BudgetSquirrelContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<UserRecord> Users { get; set; }
        public DbSet<BudgetDurationRecord> BudgetDurations { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BudgetSchema.ApplySchema(modelBuilder);
        }
    }
}
