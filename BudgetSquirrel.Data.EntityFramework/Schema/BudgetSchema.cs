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
        .HasOne(typeof(BudgetDurationRecord))
        .WithMany()
        .HasForeignKey("DurationId");
      modelBuilder.Entity<Budget>()
        .Ignore(b => b.Duration);

      modelBuilder.Entity<Budget>()
        .HasOne(b => b.ParentBudget)
        .WithMany(b => b.SubBudgets);
    }
  }
}