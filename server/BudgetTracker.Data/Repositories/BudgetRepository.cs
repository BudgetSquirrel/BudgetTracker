using BudgetTracker.Common.Models;
using BudgetTracker.Common.Models.BudgetDurations;
using BudgetTracker.Data;
using BudgetTracker.Data.Converters;
using BudgetTracker.Data.Exceptions;
using BudgetTracker.Data.Models;
using BudgetTracker.Data.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace BudgetTracker.Data.Repositories
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

            await _dbContext.Budgets.AddAsync(newBudget);
            int recordSaved = await _dbContext.SaveChangesAsync();

            if(recordSaved < 1)
            {
                throw new RepositoryException("Could not save budget to database");
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

        public async Task<Budget> GetBudget(Guid id)
        {
            BudgetModel budget = await _dbContext.Budgets.Where(x => x.Id == id).FirstAsync();
            if (budget == null)
            {
                throw new RepositoryException("Was not able to find a budget with the id " + id);
            }
            else
            {
                budget.Duration = await LoadDurationForBudget(budget);
                return BudgetConverter.ToBusinessModel(budget);
            }
        }

        public async Task<List<Budget>> GetRootBudgets(Guid userId)
        {
            List<BudgetModel> rootBudgets = await _dbContext.Budgets.Where(b => b.OwnerId == userId).ToListAsync();
            // Have to do this outside of query because EF can't do null checks.
            rootBudgets = rootBudgets.Where(b => b.ParentBudgetId == null).ToList();
            foreach (BudgetModel budgetModel in rootBudgets)
            {

            }
            List<Budget> rootBudgetsBusinessModels = BudgetConverter.ToBusinessModels(rootBudgets);
            return rootBudgetsBusinessModels;
        }

        public async Task<BudgetDurationModel> LoadDurationForBudget(BudgetModel budget)
        {
            BudgetDurationModel duration = await _dbContext.BudgetDurations.SingleAsync(d => d.Id == budget.DurationId);
            return duration;
        }

        public async Task LoadDurationsForBudgets(List<BudgetModel> budgets)
        {
            foreach (BudgetModel budgetModel in budgets)
            {
                budgetModel.Duration = await _dbContext.BudgetDurations.SingleAsync(d => d.Id == budgetModel.DurationId);
            }
        }

        public async Task<Budget> UpdateBudget(Budget budget)
        {
            BudgetModel oldBudget;

            oldBudget = await _dbContext.Budgets.SingleOrDefaultAsync(x => x.Id == budget.Id);

            if(oldBudget == null)
            {
                throw new RepositoryException("No Budget with the id: " + budget.Id + " was found.");
            }

            BudgetDurationModel oldDuration = await _dbContext.BudgetDurations.SingleAsync(d => d.Id == oldBudget.DurationId);

            oldBudget.Name = budget.Name;
            oldBudget.SetAmount = budget.SetAmount;
            oldBudget.BudgetStart = budget.BudgetStart;
            oldBudget.ParentBudgetId = budget.ParentBudgetId;

            BudgetDurationModel newDuration = GetBudgetDurationUpdated(oldDuration, budget.Duration);

            int recordSaved = await _dbContext.SaveChangesAsync();

            if(recordSaved < 1)
            {
                throw new RepositoryException("Updated " + recordSaved + " budget(s) when only 1 should have been created");
            }

            return BudgetConverter.ToBusinessModel(oldBudget);
        }

        public async Task LoadSubBudgets(Budget budget, bool recursive=false)
        {
            List<BudgetModel> subBudgetsData = await _dbContext.Budgets.Where(b => b.ParentBudgetId.Value == budget.Id).ToListAsync();
            await LoadDurationsForBudgets(subBudgetsData);

            List<Budget> subBudgets = BudgetConverter.ToBusinessModels(subBudgetsData);
            foreach (Budget subBudget in subBudgets)
            {
                subBudget.ParentBudget = budget;
                if (recursive)
                {
                    await LoadSubBudgets(subBudget);
                }
            }
            budget.SubBudgets = subBudgets;
        }

        private BudgetDurationModel GetBudgetDurationUpdated(BudgetDurationModel original, BudgetDurationBase newBudget)
        {
            if (newBudget is MonthlyBookEndedDuration)
            {
                MonthlyBookEndedDuration newBookendDuration = (MonthlyBookEndedDuration) newBudget;
                original.DurationType = DataConstants.BudgetDuration.TYPE_MONTHLY_BOOKENDS;
                original.StartDayOfMonth = newBookendDuration.StartDayOfMonth;
                original.EndDayOfMonth = newBookendDuration.EndDayOfMonth;
                original.RolloverStartDateOnSmallMonths = newBookendDuration.RolloverStartDateOnSmallMonths;
                original.RolloverEndDateOnSmallMonths = newBookendDuration.RolloverEndDateOnSmallMonths;
                original.NumberDays = -1;
            }
            else if (newBudget is MonthlyDaySpanDuration)
            {
                MonthlyDaySpanDuration newDayspanDuration = (MonthlyDaySpanDuration) newBudget;
                original.DurationType = DataConstants.BudgetDuration.TYPE_MONTHLY_SPAN;
                original.StartDayOfMonth = -1;
                original.EndDayOfMonth = -1;
                original.RolloverStartDateOnSmallMonths = false;
                original.RolloverEndDateOnSmallMonths = false;
                original.NumberDays = newDayspanDuration.NumberDays;
            }
            else
            {
                throw new RepositoryException($"The Budget duration type {newBudget.GetType().ToString()} is not a supported type.");
            }
            return original;
        }
    }
}
