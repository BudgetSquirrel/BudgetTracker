using budgettracker.common.Models;
using budgettracker.data.Converters;
using budgettracker.data.Exceptions;
using budgettracker.data.Models;
using budgettracker.data.Repositories.Interfaces;
using System;
using System.Linq;
using System.Data.Common;
using System.Threading.Tasks;

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

            using (_dbContext)
            {
                await _dbContext.Budgets.AddAsync(newBudget);
                int recordSaved = await _dbContext.SaveChangesAsync();

                if(recordSaved < 1) 
                {
                    throw new RepositoryException("Created " + recordSaved + " budget(s) when only 1 should have been created");
                }
            }
            return _budgetConverter.ToBusinessModel(newBudget);  
        }

        public async Task<Budget> UpdateBudget(Budget budget)
        {
            BudgetModel oldBudget;
            using (_dbContext)
            {
                oldBudget = _dbContext.Budgets.SingleOrDefault(x => x.Id == budget.Id);

                oldBudget.Name = budget.Name;
                oldBudget.SetAmount = budget.SetAmount;
                oldBudget.Duration = budget.Duration;
                oldBudget.BudgetStart = budget.BudgetStart;
                oldBudget.ParentBudgetId = budget.ParentBudgetId;

                int recordSaved = await _dbContext.SaveChangesAsync();

                if(recordSaved < 1) 
                {
                    throw new RepositoryException("Updated " + recordSaved + " budget(s) when only 1 should have been created");
                }
            }
            return _budgetConverter.ToBusinessModel(oldBudget);  
        }
    }
}