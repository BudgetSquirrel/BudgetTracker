using budgettracker.common;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace budgettracker.data
{
    public class BudgetTrackerContext : IdentityDbContext<User>
    {
        public DbSet<Budget> Budgets { get; set; }
    }
}