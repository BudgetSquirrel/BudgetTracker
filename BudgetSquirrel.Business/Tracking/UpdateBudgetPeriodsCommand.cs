using System;
using System.Collections.Generic;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.Business.Tracking
{
    public class UpdateBudgetPeriodsCommand
    {
        public IEnumerable<BudgetPeriod> Run(BudgetPeriod lastPeriod, BudgetDurationBase duration, DateTime date)
        {
            bool needsNewPeriod = lastPeriod == null || lastPeriod.EndDate < date;

            List<BudgetPeriod> periods = new List<BudgetPeriod>();

            if (needsNewPeriod)
            {
                DateTime newStartDate = lastPeriod?.EndDate.AddDays(1) ?? date;
                DateTime newEndDate = duration.GetEndDateFromStartDate(newStartDate);
                BudgetPeriod nextPeriod = new BudgetPeriod(newStartDate, newEndDate);
                periods.Add(nextPeriod);

                IEnumerable<BudgetPeriod> periodsUpToDate = Run(nextPeriod, duration, date);
                periods.AddRange(periodsUpToDate);
            }
            
            return periods;
        }
    }
}