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
        .HasOne(typeof(UserRecord))
        .WithMany()
        .HasForeignKey("UserId");
      modelBuilder.Entity<Budget>()
        .Ignore(b => b.User);

      modelBuilder.Entity<Budget>()
        .HasOne(b => b.Duration)
        .WithMany()
        .HasForeignKey(b => b.DurationId);

      modelBuilder.Entity<Budget>()
        .HasOne(b => b.ParentBudget)
        .WithMany(b => b.SubBudgets)
        .HasForeignKey(b => b.ParentBudgetId)
        .IsRequired(false);
    }
  }
}