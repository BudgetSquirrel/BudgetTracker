using BudgetTracker.Business.Budgeting;
using BudgetTracker.Business.BudgetPeriods;
using System;
using System.Threading.Tasks;

namespace BudgetTracker.Business.Ports.Repositories
{
    public interface IBudgetPeriodRepository
    {


        /// <summary>
        /// Gets the last budget period that was saved to the database for the
        /// given budget up until the given date. BudgetPeriods that were created
        /// after the given date (BeginDate > forDate), then it is ignored here.
        /// </summary>
        Task<BudgetPeriod> GetLastSavedBeforeDate(Budget budget, DateTime forDate);

        Task<BudgetPeriod> CreateBudgetPeriod(BudgetPeriod toCreate);
    }
}
