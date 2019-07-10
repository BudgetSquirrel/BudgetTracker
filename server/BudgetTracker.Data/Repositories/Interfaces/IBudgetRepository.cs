using BudgetTracker.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetTracker.Data.Repositories.Interfaces
{
    public interface IBudgetRepository
    {
        /// <summary>
        /// <p>
        /// Saves the new budget to the database will return the newly created budget
        /// model but will throw exceptions if something fails will be caught in
        /// <see cref="BudgetApi"/>
        /// </p>
        /// </summary>
        /// <param name="budget"><see cref="Budget"/></param>
        Task<Budget> CreateBudget(Budget budget);

        /// <summary>
        /// <p>
        /// Updates a budget based off their id, will return the update budget
        /// modle but will throw an exception if something fails will be caught in 
        /// <see cref="BudgetApi"/>
        /// </p>
        /// </summary>
        Task<Budget> UpdateBudget(Budget budget);
        
        /// Deletes all Budgets that match the given ids. All ids that do not
        /// match a Budget record or couldn't be deleted will be returned in a
        /// <see cref="BudgetTracker.Data.Exceptions.RepositoryException" />.
        /// This error will not be thrown until all budgets that can be deleted
        /// have been deleted.
        /// </p>
        /// </summary>
        Task DeleteBudgets(List<Guid> Ids);

        /// <summary>
        /// <p>
        /// Will return a single budget based off the given id
        /// </p>
        /// </summary>
        Task<Budget> GetBudget(Guid id);

        /// <summary>
        /// <p>
        /// Fetches all root budgets for the given user. A root budget is one
        /// that has no parent budget.
        /// </p>
        /// </summary>
        Task<List<Budget>> GetRootBudgets(Guid userId);
    }
}
