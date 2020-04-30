using System;

namespace BudgetSquirrel.Data.EntityFramework.Models
{
  public class BudgetRecord
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public double? PercentAmount { get; set; }

    public decimal? SetAmount { get; set; }

    public decimal FundBalance { get; set; }

    public DateTime BudgetStart { get; set; }

    public Guid UserId { get; set; }

    public UserRecord User { get; set; }
  }
}