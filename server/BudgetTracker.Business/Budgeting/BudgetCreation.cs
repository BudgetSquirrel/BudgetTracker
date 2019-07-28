using BudgetTracker.Common;
using BudgetTracker.Common.Models;
using BudgetTracker.Business.Ports.Exceptions;
using BudgetTracker.Business.Ports.Repositories;
using System.Threading.Tasks;

namespace BudgetTracker.Business.Budgeting
{
    public class BudgetCreation
    {
        public static async Task<Budget> CreateBudgetForUser(Budget budgetCreateObject, User user, IBudgetRepository budgetRepository)
        {
            budgetCreateObject.Owner = user;

            if (!budgetCreateObject.IsRootBudget)
            {
                budgetCreateObject.ParentBudget = await budgetRepository.GetBudget(budgetCreateObject.ParentBudgetId.Value);
                budgetCreateObject.Duration = budgetCreateObject.ParentBudget.Duration;
            }

            budgetCreateObject.SetAmount = budgetCreateObject.CalculateBudgetSetAmount();

            budgetCreateObject = await budgetRepository.CreateBudget(budgetCreateObject);

            return budgetCreateObject;
        }
    }
}
