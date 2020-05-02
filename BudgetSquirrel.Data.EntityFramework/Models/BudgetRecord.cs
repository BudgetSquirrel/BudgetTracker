using System;
using System.Collections.Generic;

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

    public Guid DurationId { get; set; }

    public BudgetDurationRecord Duration { get; set; }

    public Guid? ParentBudgetId { get; set; }

    public BudgetRecord ParentBudget { get; set; }

    public List<BudgetRecord> SubBudgets { get; set; }
  }
}