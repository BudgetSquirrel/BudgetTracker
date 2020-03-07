using System;

namespace BudgetSquirrel.Business.BudgetPlanning
{
    public class DaySpanDuration : BudgetDurationBase
    {
        /// <summary>
        /// <p>
        /// Specifies the number of days that this budget will span.
        /// </p>
        /// </summary>
        public int NumberDays { get; private set; }

        public DaySpanDuration(int numberDays)
        {
            if (numberDays < 1)
                throw new InvalidOperationException("Day span durations must span 1 or more days");
            NumberDays = numberDays;
        }

        public DaySpanDuration(Guid id, int numberDays)
            : base(id)
        {
            if (numberDays < 1)
                throw new InvalidOperationException("Day span durations must span 1 or more days");
            NumberDays = numberDays;
        }

        public override DateTime GetEndDateFromStartDate(DateTime start)
        {
            return start.AddDays(NumberDays);
        }
    }
}
