using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Data.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetSquirrel.Data.EntityFramework.Schema
{
  public static class FundSchema
  {
    public static void ApplySchema(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Fund>()
        .HasOne(typeof(UserRecord))
        .WithMany()
        .HasForeignKey("UserId");
      modelBuilder.Entity<Fund>()
        .Ignore(b => b.User);

      modelBuilder.Entity<Fund>()
        .HasOne(f => f.Duration)
        .WithMany()
        .HasForeignKey(f => f.DurationId);

      modelBuilder.Entity<Fund>()
        .HasOne(f => f.ParentFund)
        .WithMany(b => b.SubFunds)
        .HasForeignKey(b => b.ParentFundId)
        .OnDelete(DeleteBehavior.Cascade)
        .IsRequired(false);

      modelBuilder.Entity<Fund>()
        .Ignore(f => f.HistoricalBudgets);
    }
  }
}