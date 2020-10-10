using System;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.Business
{
    public class BudgetPeriod
    {
        public Guid Id { get; private set; }        

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public Budget RootBudget { get; set; }

        // Jan - Budget
        // Feb - Budget <- Only edit in Jan / Until finalized in Feb
        // March - Created when Feb starts

        public BudgetPeriod(DateTime startDate, DateTime endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        private BudgetPeriod() {}
    }
}