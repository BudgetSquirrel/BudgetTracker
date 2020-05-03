using System;

namespace BudgetSquirrel.Data.EntityFramework.Models
{
  public class BudgetDurationRecord
  {
    public Guid Id { get; set; }

    /** Fields for day span duration */
    
    public int? NumberDays { get; set; }

    /** Fields for monthly bookended duration */

    public int? EndDayOfMonth { get; set; }
    public bool? RolloverEndDateOnSmallMonths { get; set; }
  }
}