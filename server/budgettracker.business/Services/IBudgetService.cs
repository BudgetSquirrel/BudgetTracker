using budgettracker.common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace budgettracker.business.Services
{
    /// <summary>
    /// Interface to the budget service <see cref="BudgetService.cs"/>
    /// </summary>
    public interface IBudgetService
    {
        /// <summary>
        /// Creates a budget for the user
        /// </summary>
        /// <param name="budget">the budget to be saved</param>
        /// <returns></returns>
        Task CreateBudget(Budget budget);
    }
}
