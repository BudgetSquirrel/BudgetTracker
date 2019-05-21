using budgettracker.common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace budgettracker.business.Services
{
    /// <summary>
    /// Will handle all business logic, mapping, and calls to the data layer for budgets. 
    /// </summary>
    public class BudgetService : IBudgetService
    {
        /// <summary>
        /// Creates a budget for the user
        /// </summary>
        /// <param name="budget">the budget to be saved</param>
        /// <returns></returns>
        public Task CreateBudget(Budget budget)
        {
            // TODO: Make call to Data layer to save the budget
            throw new NotImplementedException();
        }
    }
}
