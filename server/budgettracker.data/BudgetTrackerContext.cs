using budgettracker.common.Models;
using budgettracker.data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace budgettracker.data
{
    public class BudgetTrackerContext : IdentityDbContext<UserModel>
    {
        public BudgetTrackerContext()
        {
        }

        public BudgetTrackerContext(DbContextOptions options)
            :base(options)
        {
            
        }

        public DbSet<Budget> Budgets { get; set; }
        
    }
}