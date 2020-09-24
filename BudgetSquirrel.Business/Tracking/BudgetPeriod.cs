using System;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.Business.Tracking
{
    public class BudgetPeriod
    {
        public Guid Id { get; private set; }
        public Guid BudgetId { get; private set; }
        public Budget Budget { get; private set; }
        
        /// <summary>
        /// The date in which the user finalized their budget 
        /// unable to edit any values until the edit period comes available.
        /// <see cref="IsEditable"/>
        /// </summary>
        public DateTime? DateFinalized { get; private set; }

        /// <summary>
        /// The period in which the user is able to edit their amount
        /// <see cref="DateFinalized"/>
        /// </summary>
        public bool IsEditable
        {
            get
            {
                // TODO
                return true;
            }
        }

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public BudgetPeriod(Budget budget, DateTime startDate, DateTime endDate)
        {
            this.Budget = budget;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public void SetFinalizedDate()
        {
            this.DateFinalized = DateTime.Now;
        }

        private BudgetPeriod() {}
    }
}