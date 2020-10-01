using System;
using System.Collections.Generic;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.Business.Tracking
{
    public class UpdateBudgetPeriodsCommand
    {
        public IEnumerable<BudgetPeriod> Run(Budget lastBudget, DateTime date)
        {
            BudgetPeriod lastPeriod = lastBudget.BudgetPeriod;
            BudgetDurationBase duration = lastBudget.Fund.Duration;
            bool needsNewPeriod = lastPeriod == null || lastPeriod.EndDate < date;

            List<BudgetPeriod> periods = new List<BudgetPeriod>();

            if (needsNewPeriod)
            {
                DateTime newStartDate = lastPeriod?.EndDate.AddDays(1) ?? date;
                DateTime newEndDate = duration.GetEndDateFromStartDate(newStartDate);

                // TODO: Initialize the new budgets for next period
                Budget budget = null; // temporary... needs completion
                BudgetPeriod nextPeriod = new BudgetPeriod(newStartDate, newEndDate);
                periods.Add(nextPeriod);

                IEnumerable<BudgetPeriod> periodsUpToDate = Run(budget, date);
                periods.AddRange(periodsUpToDate);
            }
            
            return periods;
        }
    }
}