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
            /* add one less because the duration means it lasts that
             * many days... not that you add that many days.
             * So if the duration is 4 days and start date is on
             * the 3rd of a month, then you would only add 3 days
             * for the end date to get the 6th. This way, the period
             * spans 4 days; 3, 4, 5, 6.
             */ 
            return start.AddDays(NumberDays - 1);
        }
    }
}
