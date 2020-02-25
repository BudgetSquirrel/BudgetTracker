using System;

namespace BudgetSquirrel.Business.BudgetPeriods
{
    public abstract class BudgetDurationBase
    {
        public Guid Id { get; set; }

        public abstract DateTime GetEndDateFromStartDate(DateTime startDate);
    }
}
