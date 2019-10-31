using BudgetTracker.Business.Budgeting;
using BudgetTracker.Business.Ports.Repositories;
using System;
using System.Threading.Tasks;

namespace BudgetTracker.Business.BudgetPeriods
{
    public class BudgetPeriodCalculator
    {
        private IBudgetPeriodRepository _budgetPeriodRepository;
        private IBudgetRepository _budgetRepository;

        public BudgetPeriodCalculator(IBudgetPeriodRepository budgetPeriodRepository,
            IBudgetRepository budgetRepository)
        {
            _budgetPeriodRepository = budgetPeriodRepository;
            _budgetRepository = budgetRepository;
        }

        /// <summary>
        /// Gets the budget period for the given date. If it doesn't exist, a
        /// new one is calculated from the last saved BudgetPeriod, saved to the
        /// repository and returned here.
        /// </summary>
        public async Task<BudgetPeriod> GetOrCreateForDate(Budget budget, DateTime forDate)
        {
            await _budgetRepository.LoadSubBudgets(budget, true);
            BudgetPeriod lastSaved = await _budgetPeriodRepository.GetLastSavedBeforeDate(budget, forDate);

            BudgetPeriod currentPeriod = lastSaved;
            DateTime nextStartDate = currentPeriod?.EndDate.AddDays(1) ?? budget.BudgetStart;
            while (nextStartDate <= forDate)
            {
                if (currentPeriod != null)
                    await budget.ClosePeriod(currentPeriod, _budgetRepository);

                BudgetPeriod newPeriod = new BudgetPeriod(budget, nextStartDate);
                currentPeriod = await _budgetPeriodRepository.CreateBudgetPeriod(newPeriod);

                nextStartDate = currentPeriod.EndDate.AddDays(1);
            }

            return currentPeriod;
        }
    }
}
