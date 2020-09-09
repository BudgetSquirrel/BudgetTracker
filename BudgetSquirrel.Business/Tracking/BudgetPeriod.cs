using System;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.Business.Tracking
{
    public class BudgetPeriod
    {
        public Guid Id { get; private set; }
        public Guid BudgetId { get; private set; }
        public Budget Budget { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public BudgetPeriod(Budget budget, DateTime startDate, DateTime endDate)
        {
            this.Budget = budget;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        private BudgetPeriod() {}
    }
}