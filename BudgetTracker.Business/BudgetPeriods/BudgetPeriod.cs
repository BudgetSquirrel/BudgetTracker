using BudgetTracker.Business.Budgeting;
using System;

namespace BudgetTracker.Business.BudgetPeriods
{
    public class BudgetPeriod
    {
        /// <summary>
        /// The root budget related to this budget period.
        /// </summary>
        public Budget RootBudget { get; set; }

        /// <summary>
        /// The root budget id related to this budget period.
        /// </summary>
        public Guid RootBudgetId { get; set; }

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
