using budgettracker.data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace budgettracker.data
{
    public class BudgetTrackerContext : IdentityDbContext<UserModel>
    {
        public BudgetTrackerContext(DbContextOptions options)
            :base(options)
        {
            
        }
    }
}