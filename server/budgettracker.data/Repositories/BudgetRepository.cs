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

                if(recordSaved != 1)
                {
                    throw new RepositoryException("Created " + recordSaved + " budget(s) when only 1 should have been created");
                }
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
    }
}
