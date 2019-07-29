using BudgetTracker.Web.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Web.Data
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
    }
}
