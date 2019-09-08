using BudgetTracker.Data.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Data.EntityFramework
{
    public class BudgetTrackerContext : DbContext
    {
        public BudgetTrackerContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<BudgetModel> Budgets { get; set; }
        public DbSet<BudgetDurationModel> BudgetDurations { get; set; }
        public DbSet<BudgetPeriodModel> BudgetPeriods { get; set; }
        public DbSet<TransactionModel> Transactions { get; set; }
    }
}
