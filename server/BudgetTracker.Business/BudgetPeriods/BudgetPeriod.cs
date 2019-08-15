using System;

namespace BudgetTracker.Common.Models
{
    public class BudgetPeriod 
    {
        /// <summary>
        /// The budget id related to this budget period.
        /// </summary>
        public Guid BudgetId { get; set; }

        /// <summary>
        /// The start date of the budget period
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The end date of the budget period
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}