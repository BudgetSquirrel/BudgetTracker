using System;

namespace BudgetSquirrel.Business.BudgetPlanning
{
    /// <summary>
    /// <p>
    /// Represents a Budget Duration that starts and ends at specified days in
    /// a month.
    /// </p>
    /// </summary>
    public class MonthlyBookEndedDuration : BudgetDurationBase
    {
        /// <summary>
        /// <p>
        /// The day in the month that each period ends on. For example, if this
        /// is 28, then each period will begin on the 28th day of the month like
        /// so: (January 28th, February 28th, March 28th and so on).
        /// </p>
        /// </summary>
        public int EndDayOfMonth { get; set; }

        /// <summary>
        /// <p>
        /// Determines whether or not to roll the end date over the remaining
        /// number of days if the date falls outside of the range of days in the
        /// current month. If this is false, this will instead be set to the
        /// last day in the month.
        /// </p>
        /// <p>
        /// For example, if this is true, and the end date is 30 and the month
        /// is February on a leap year (28 days in the month), then the actual
        /// end date will be set to March 2nd (2 days remaining after 28th).
        /// However, in the same scenario, if this is false, then the actual
        /// end date will simply be set to February 28th.
        /// </p>
        /// </summary>
        public bool RolloverEndDateOnSmallMonths { get; set; }

        public MonthlyBookEndedDuration(int endDay, bool rolloverEndDateOnSmallMonths)
        {
            if (endDay < 1 || endDay > 31)
                throw new InvalidOperationException("Monthly bookended durations must be a valid date (1 - 31)");
            EndDayOfMonth = endDay;
            RolloverEndDateOnSmallMonths = rolloverEndDateOnSmallMonths;
        }

        public MonthlyBookEndedDuration(Guid id, int endDay, bool rolloverEndDateOnSmallMonths)
            : base(id)
        {
            if (endDay < 1 || endDay > 31)
                throw new InvalidOperationException("Monthly bookended durations must be a valid date (1 - 31)");
            EndDayOfMonth = endDay;
            RolloverEndDateOnSmallMonths = rolloverEndDateOnSmallMonths;
        }

        private MonthlyBookEndedDuration() {}

        public override DateTime GetEndDateFromStartDate(DateTime start)
        {
            int year = start.Year;
            int month = start.Month;
            int endDay = EndDayOfMonth;

            if (endDay <= start.Day)
            {
                month ++;
            }

            bool endDateIsInvalid = DateTime.DaysInMonth(year, month) < endDay;
            if (endDateIsInvalid && RolloverEndDateOnSmallMonths)
            {
                month ++;
                endDay = 1;
            }
            else if (endDateIsInvalid)
            {
                endDay = DateTime.DaysInMonth(year, month);
            }

            return new DateTime(year, month, endDay);
        }
    }
}
