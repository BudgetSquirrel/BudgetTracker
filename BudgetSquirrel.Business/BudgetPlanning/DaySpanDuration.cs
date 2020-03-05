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
            NumberDays = numberDays;
        }

        public DaySpanDuration(Guid id, int numberDays)
            : base(id)
        {
            NumberDays = numberDays;
        }
    }
}
