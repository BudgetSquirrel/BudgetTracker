using budgettracker.common.Models;
using budgettracker.data.Converters;
using budgettracker.data.Exceptions;
using budgettracker.data.Models;
using budgettracker.data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace budgettracker.data.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {

        private readonly BudgetTrackerContext _dbContext;

        public BudgetRepository(BudgetTrackerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Budget> CreateBudget(Budget budget)
        {
            BudgetModel newBudget = BudgetConverter.ToDataModel(budget);

            using (_dbContext)
            {
                await _dbContext.Budgets.AddAsync(newBudget);
                int recordSaved = await _dbContext.SaveChangesAsync();

                if(recordSaved < 1)
                {
                    throw new RepositoryException("Could not save budget to database");
                }
            }
            return BudgetConverter.ToBusinessModel(newBudget);
        }

        public async Task DeleteBudgets(List<Guid> Ids)
        {
            List<BudgetModel> toDelete = await _dbContext.Budgets.Where(b => Ids.Contains(b.Id)).ToListAsync();
            List<Guid> nonExistant = Ids.Where(id => !toDelete.Select(b => b.Id).Contains(id)).ToList();
            List<Guid> erroredBudgets = nonExistant;

            _dbContext.RemoveRange(toDelete);
            if (await _dbContext.SaveChangesAsync() < toDelete.Count())
            {
                List<Guid> notDeleted = await (from b in _dbContext.Budgets
                                                where Ids.Contains(b.Id)
                                                select b.Id).ToListAsync();
                erroredBudgets.AddRange(notDeleted);
            }
            if (erroredBudgets.Any())
            {
                string errorIds = String.Join(",", erroredBudgets.ToArray());
                throw new RepositoryException("NOT_DELETED:" + errorIds);
            }
        }

        public async Task<List<Budget>> GetRootBudgets(Guid userId)
        {
            List<BudgetModel> rootBudgets = await _dbContext.Budgets.Where(b => b.OwnerId == userId).ToListAsync();
            // Have to do this outside of query because EF can't do null checks.
            rootBudgets = rootBudgets.Where(b => b.ParentBudgetId == null).ToList();
            foreach (BudgetModel budgetModel in rootBudgets)
            {
                budgetModel.Duration = await LoadDurationForBudget(budgetModel);
            }
            List<Budget> rootBudgetsBusinessModels = BudgetConverter.ToBusinessModels(rootBudgets);
            return rootBudgetsBusinessModels;
        }

        public async Task<BudgetDurationModel> LoadDurationForBudget(BudgetModel budget)
        {
            BudgetDurationModel duration = await _dbContext.BudgetDurations.SingleAsync(d => d.Id == budget.DurationId);
            return duration;
        }
    }
}
