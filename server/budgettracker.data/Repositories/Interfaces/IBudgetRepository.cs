using budgettracker.common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace budgettracker.data.Repositories.Interfaces
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
        /// Deletes all Budgets that match the given ids. All ids that do not
        /// match a Budget record or couldn't be deleted will be returned in a
        /// <see cref="budgettracker.data.Exceptions.RepositoryException" />.
        /// This error will not be thrown until all budgets that can be deleted
        /// have been deleted.
        /// </p>
        /// </summary>
        Task DeleteBudgets(List<Guid> Ids);

        /// <summary>
        /// <p>
        /// Fetches all root budgets for the given user. A root budget is one
        /// that has no parent budget.
        /// </p>
        /// </summary>
        Task<IEnumerable<Budget>> GetRootBudgets(Guid userId);
    }
}
