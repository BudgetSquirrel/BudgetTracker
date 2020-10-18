using BudgetSquirrel.Business;
using Microsoft.EntityFrameworkCore;

namespace BudgetSquirrel.Data.EntityFramework.Schema
{
    public static class BudgetPeriodSchema
    {
        public static void ApplySchema(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetPeriod>()
                .Ignore(b => b.RootBudget);
        }
    }
}