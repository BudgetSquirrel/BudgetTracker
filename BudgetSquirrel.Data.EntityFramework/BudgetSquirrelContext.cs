using BudgetSquirrel.Business.Tracking;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Data.EntityFramework.Models;
using BudgetSquirrel.Data.EntityFramework.Schema;
using Microsoft.EntityFrameworkCore;
using BudgetSquirrel.Business;

namespace BudgetSquirrel.Data.EntityFramework
{
    public class BudgetSquirrelContext : DbContext
    {
        public BudgetSquirrelContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<UserRecord> Users { get; set; }
        public DbSet<BudgetDurationBase> BudgetDurations { get; set; }
        public DbSet<MonthlyBookEndedDuration> MonthlyBookEndedDurations { get; set; }
        public DbSet<DaySpanDuration> DaySpanDurations { get; set; }
        public DbSet<BudgetPeriod> BudgetPeriods { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Fund> Funds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            FundSchema.ApplySchema(modelBuilder);
            BudgetSchema.ApplySchema(modelBuilder);
        }
    }
}
