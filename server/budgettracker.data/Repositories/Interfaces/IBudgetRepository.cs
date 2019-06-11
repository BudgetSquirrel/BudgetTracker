using System.Threading.Tasks;
using budgettracker.common.Models;

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
    }
}