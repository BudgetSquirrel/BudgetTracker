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
        /// The day in the month that each period starts on. For example, if
        /// this is 3, then each period will begin on the 3rd day of the month
        /// like so: (January 3rd, February 3rd, March 3rd and so on).
        /// </p>
        /// </summary>
        public int StartDayOfMonth { get; set; }

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

        public MonthlyBookEndedDuration(int startDay, int endDay, bool rolloverEndDateOnSmallMonths)
        {
            StartDayOfMonth = startDay;
            EndDayOfMonth = endDay;
            RolloverEndDateOnSmallMonths = rolloverEndDateOnSmallMonths;
        }

        public MonthlyBookEndedDuration(Guid id, int startDay, int endDay, bool rolloverEndDateOnSmallMonths)
            : base(id)
        {
            StartDayOfMonth = startDay;
            EndDayOfMonth = endDay;
            RolloverEndDateOnSmallMonths = rolloverEndDateOnSmallMonths;
        }

        public override DateTime GetEndDateFromStartDate(DateTime start)
        {
            int year = start.Year;
            int month = start.Month;
            int day = EndDayOfMonth;

            bool isNextMonth = StartDayOfMonth >= day;
            if (isNextMonth)
                month ++;

            if (month > 12)
            {
                month = 1;
                year ++;
            }

            bool endDateIsInvalid = DateTime.DaysInMonth(year, month) <= day;
            if (!endDateIsInvalid && RolloverEndDateOnSmallMonths)
            {
                month ++;
                day = 1;
            }

            return new DateTime(year, month, day);
        }
    }
}
