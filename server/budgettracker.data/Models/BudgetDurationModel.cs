using System;

namespace budgettracker.data.Models
{
    /// <summary>
    /// <p>
    /// Contains information about the durration of a budget period. This is how
    /// a budget is marked as a monthly budget or a budget that spans a specific
    /// number of days.
    /// </p>
    /// <p>
    /// This model contains all fields for all types of budget durrations. This
    /// is because Databases lack abstraction and inheritance. So the type of
    /// durration that each instance is depends on the combination of what
    /// fields are used.
    /// </p>
    /// <p>
    /// A BudgetDuration can be 1 of the following 2 types:
    /// </p>
    /// <p>
    /// - A Date to date durration: These durrations start and end on specified
    /// days of the month. If the end date is smaller than the start date, then
    /// it is assumed that it is a date in the next month after the start date.
    /// In the case where either of those dates don't exist on the month
    /// (Ex. Leap years), this durration can be configured to do one of the
    /// following with that date:
    ///
    ///     - Roll the date over the remaining number of days into the next
    ///         month
    ///     - Set the date to the last day in the month.
    /// </p>
    /// <p>
    /// - A duration for a specified number of days. These durrations start at
    /// a date and last as many days as specified by the BudgetDuration object.
    /// </p>
    /// </summary>
    public class BudgetDurationModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// <p>
        /// The type of duration that this is. Different types have different
        /// combinations of fields that are filled out. Examples of type might
        /// be "MONTHLY_BOOKENDS" for start/end date types or "MONTHLY_SPAN" for
        /// durations that span a number of days.
        /// </p>
        /// <p>
        /// Options for this field are the TYPE_* constants found in the
        /// class <see cref="budgettracker.data.DataConstants.BudgetDuration" />
        /// </p>
        /// </summary>
        public string DurrationType { get; set; }

        #region Monthly Budget Fields
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
        /// Determines whether or not to roll the start date over the remaining
        /// number of days if the date falls outside of the range of days in the
        /// current month. If this is false, this will instead be set to the
        /// last day in the month.
        /// </p>
        /// <p>
        /// For example, if this is true, and the start date is 30 and the month
        /// is February on a leap year (28 days in the month), then the actual
        /// start date will be set to March 2nd (2 days remaining after 28th).
        /// However, in the same scenario, if this is false, then the actual
        /// start date will simply be set to February 28th.
        /// </p>
        /// </summary>
        public bool RolloverStartDateOnSmallMonths { get; set; }

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
        #endregion

        #region Days Spanning Fields
        /// <summary>
        /// <p>
        /// Specifies the number of days that this budget will span.
        /// </p>
        /// </summary>
        public int NumDays { get; set; }
        #endregion
    }
}
