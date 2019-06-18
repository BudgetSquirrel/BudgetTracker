using budgettracker.data.Models;
using Microsoft.EntityFrameworkCore;

namespace budgettracker.data
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
    }
}
