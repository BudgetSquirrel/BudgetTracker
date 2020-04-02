using BudgetSqurrel.Data.EntityFramework.Models;
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
    }
}
