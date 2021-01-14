using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Data.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetSquirrel.Data.EntityFramework.Schema
{
  public static class BudgetSchema
  {
    public static void ApplySchema(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Budget>()
        .Ignore(b => b.SubBudgets)
        .Ignore(b => b.ParentBudget);
      
      modelBuilder.Entity<Budget>()
        .HasIndex(b => b.BudgetPeriodId)
        .IsUnique(false);
    }
  }
}