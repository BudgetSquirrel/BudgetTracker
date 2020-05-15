using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Data.EntityFramework.Models;

namespace BudgetSquirrel.Data.EntityFramework.Converters
{
  public static class BudgetDurationConverter
  {
    public static BudgetDurationBase ToDomainModel(BudgetDurationRecord durationData)
    {
      if (durationData.NumberDays.HasValue)
      {
        return new DaySpanDuration(durationData.Id, durationData.NumberDays.Value);
      }
      else
      {
        return new MonthlyBookEndedDuration(durationData.Id, durationData.EndDayOfMonth.Value, durationData.RolloverEndDateOnSmallMonths.Value);
      }
    }
  
  
    public static BudgetDurationRecord ToDataModel(BudgetDurationBase duration)
    {
      if (duration is DaySpanDuration)
      {
        return new BudgetDurationRecord()
        {
          Id = duration.Id,
          NumberDays = ((DaySpanDuration) duration).NumberDays
        };
      }
      else
      {
        return new BudgetDurationRecord()
        {
          Id = duration.Id,
          EndDayOfMonth = ((MonthlyBookEndedDuration) duration).EndDayOfMonth,
          RolloverEndDateOnSmallMonths = ((MonthlyBookEndedDuration) duration).RolloverEndDateOnSmallMonths
        };
      }
    }
  }
}