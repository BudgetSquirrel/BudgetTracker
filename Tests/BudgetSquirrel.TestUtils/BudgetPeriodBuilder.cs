using System;
using BudgetSquirrel.Business;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.TestUtils
{
    public class BudgetPeriodBuilder : IBudgetPeriodBuilder
    {
        private BudgetDurationBase duration;

        private DateTime startDate;

        public BudgetPeriodBuilder()
        {
        }

        private void InitRandom()
        {
            this.startDate = DateTime.Now;
        }

        public IBudgetPeriodBuilder ForDuration(BudgetDurationBase duration)
        {
            this.duration = duration;
            return this;
        }

        public IBudgetPeriodBuilder SetStartDate(DateTime startDate)
        {
            throw new NotImplementedException();
        }

        public BudgetPeriod Build()
        {
            if (this.duration == null)
            {
                throw new InvalidOperationException("You must set the duration using ForDuration(BudgetDurationBase) before building the period");
            }
            DateTime endDate = this.duration.GetEndDateFromStartDate(this.startDate);
            BudgetPeriod period = new BudgetPeriod(this.startDate, endDate);
            return period;
        }
    }
}