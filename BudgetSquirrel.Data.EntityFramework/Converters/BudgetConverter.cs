using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Data.EntityFramework.Models;

namespace BudgetSquirrel.Data.EntityFramework.Converters
{
  public static class BudgetConverter
  {
    public static BudgetRecord ToDataModel(Budget budget)
    {
      BudgetDurationRecord duration = new BudgetDurationRecord() { Id = budget.Duration.Id };
      if (budget.Duration is MonthlyBookEndedDuration)
      {
        duration.EndDayOfMonth = ((MonthlyBookEndedDuration) budget.Duration).EndDayOfMonth;
        duration.RolloverEndDateOnSmallMonths = ((MonthlyBookEndedDuration) budget.Duration).RolloverEndDateOnSmallMonths;
      }
      else
      {
        duration.NumberDays = ((DaySpanDuration) budget.Duration).NumberDays;
      }

      BudgetRecord record = new BudgetRecord()
      {
        Id = budget.Id,
        Name = budget.Name,
        FundBalance = budget.FundBalance,
        PercentAmount = budget.PercentAmount,
        SetAmount = budget.SetAmount,
        BudgetStart = budget.BudgetStart,
        Duration = duration
      };
      return record;
    }

    public static Budget ToDomainModel(BudgetRecord record)
    {
      BudgetDurationBase duration;
      if (record.Duration.NumberDays.HasValue)
      {
        duration = new MonthlyBookEndedDuration(record.Id, record.Duration.EndDayOfMonth.Value, record.Duration.RolloverEndDateOnSmallMonths.Value);
      }
      else
      {
        duration = new DaySpanDuration(record.Id, record.Duration.NumberDays.Value);
      }

      Budget budget = new Budget(record.Id, record.Name, record.FundBalance, duration, record.BudgetStart);
      return budget;
    }
  }
}