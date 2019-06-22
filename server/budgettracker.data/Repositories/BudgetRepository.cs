using budgettracker.common.Models;
using budgettracker.common.Models.BudgetDurations;
using budgettracker.data;
using budgettracker.data.Converters;
using budgettracker.data.Exceptions;
using budgettracker.data.Models;
using budgettracker.data.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace budgettracker.data.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {

        private readonly BudgetTrackerContext _dbContext;
        private readonly BudgetConverter _budgetConverter;

        public BudgetRepository(BudgetTrackerContext dbContext)
        {
            _dbContext = dbContext;
            _budgetConverter = new BudgetConverter();
        }

        public async Task<Budget> CreateBudget(Budget budget)
        {
            BudgetModel newBudget = _budgetConverter.ToDataModel(budget);

            await _dbContext.Budgets.AddAsync(newBudget);
            int recordSaved = await _dbContext.SaveChangesAsync();

            if(recordSaved < 1)
            {
                throw new RepositoryException("Could not save budget to database");
            }

            return _budgetConverter.ToBusinessModel(newBudget);
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
                return _budgetConverter.ToBusinessModel(budget);
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

            return _budgetConverter.ToBusinessModel(oldBudget);
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
