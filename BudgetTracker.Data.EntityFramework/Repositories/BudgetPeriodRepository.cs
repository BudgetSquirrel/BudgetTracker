using BudgetTracker.Business.Budgeting;
using BudgetTracker.Business.BudgetPeriods;
using BudgetTracker.Business.Ports.Repositories;
using BudgetTracker.Data.EntityFramework;
using BudgetTracker.Data.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace BudgetTracker.Data.EntityFramework.Repositories
{
    public class BudgetPeriodRepository : IBudgetPeriodRepository
    {
        private BudgetTrackerContext _context;

        public BudgetPeriodRepository(BudgetTrackerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the last budget period that was saved to the database for the
        /// given budget up until the given date. BudgetPeriods that were created
        /// after the given date (BeginDate > forDate), then it is ignored here.
        /// </summary>
        public async Task<BudgetPeriod> GetLastSavedBeforeDate(Budget budget, DateTime forDate)
        {
            BudgetPeriodModel periodModel = await _context.BudgetPeriods.Where(p => p.RootBudgetId == budget.Id &&
                                                                                        p.StartDate <= forDate)
                                                                        .OrderBy(p => p.StartDate)
                                                                        .LastOrDefaultAsync();
            return periodModel?.ToDomain() ?? null;
        }

        public async Task<BudgetPeriod> CreateBudgetPeriod(BudgetPeriod toCreate)
        {
            BudgetPeriodModel periodModel = new BudgetPeriodModel(toCreate);
            _context.BudgetPeriods.Add(periodModel);
            await _context.SaveChangesAsync();
            return periodModel.ToDomain();
        }
    }
}
