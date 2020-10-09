using System;
using System.Collections.Generic;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.Business
{
    public class UpdateBudgetPeriodsCommand
    {
        private Budget lastBudget;
        private DateTime currentDate;

        public UpdateBudgetPeriodsCommand(Budget lastBudget, DateTime currentDate)
        {
            this.lastBudget = lastBudget;
            this.currentDate = currentDate;
        }
        
        public IEnumerable<BudgetPeriod> Run()
        {
            BudgetPeriod lastPeriod = this.lastBudget.BudgetPeriod;
            BudgetDurationBase duration = this.lastBudget.Fund.Duration;
            bool needsNewPeriod = lastPeriod == null || lastPeriod.EndDate < this.currentDate;

            List<BudgetPeriod> periods = new List<BudgetPeriod>();

            if (needsNewPeriod)
            {
                DateTime newStartDate = lastPeriod?.EndDate.AddDays(1) ?? this.currentDate;
                DateTime newEndDate = duration.GetEndDateFromStartDate(newStartDate);

                // TODO: Initialize the new budgets for next period
                Budget budget = null; // temporary... needs completion
                BudgetPeriod nextPeriod = new BudgetPeriod(newStartDate, newEndDate);
                periods.Add(nextPeriod);

                UpdateBudgetPeriodsCommand recursiveCommand = new UpdateBudgetPeriodsCommand(budget, this.currentDate);
                IEnumerable<BudgetPeriod> periodsUpToDate = Run();
                periods.AddRange(periodsUpToDate);
            }
            
            return periods;
        }
    }
}