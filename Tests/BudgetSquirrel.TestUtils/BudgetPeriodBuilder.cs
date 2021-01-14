using System;
using BudgetSquirrel.Business;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.TestUtils
{
    public class BudgetPeriodBuilder : IBudgetPeriodBuilder
    {
        private Budget rootBudget;

        private DateTime startDate;

        public BudgetPeriodBuilder()
        {
        }

        private void InitRandom()
        {
            this.startDate = DateTime.Now;
        }

        public IBudgetPeriodBuilder ForRootBudget(Budget rootBudget)
        {
            this.rootBudget = rootBudget;
            return this;
        }

        public IBudgetPeriodBuilder SetStartDate(DateTime startDate)
        {
            this.startDate = startDate;
            return this;
        }

        public BudgetPeriod Build()
        {
            if (this.rootBudget == null)
            {
                throw new InvalidOperationException("You must set the Budget using ForRootBudget(Budget) before building the period");
            }
            DateTime endDate = this.rootBudget.Fund.Duration.GetEndDateFromStartDate(this.startDate);
            BudgetPeriod period = new BudgetPeriod(this.rootBudget, this.startDate, endDate);
            return period;
        }
    }
}