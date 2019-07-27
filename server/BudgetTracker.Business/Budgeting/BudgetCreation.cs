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

            if (!budgetCreateObject.isRootBudget)
            {
                budgetCreateObject.ParentBudget = await budgetRepository.GetBudget(budgetCreateObject.ParentBudgetId.Value);
                budgetCreateObject.Duration = budgetCreateObject.ParentBudget.Duration;
            }

            budgetCreateObject.SetAmount = CalculateBudgetSetAmount(budgetCreateObject);

            budgetCreateObject = await budgetRepository.CreateBudget(budgetCreateObject);

            return budgetCreateObject;
        }

        public static decimal CalculateBudgetSetAmount(Budget budget)
        {
            decimal newBudgetAmount = default(decimal);
            if (budget.IsPercentBasedBudget)
            {
                newBudgetAmount = budget.ParentBudget.SetAmount.Value * (decimal) budget.PercentAmount.Value;
            }
            else
            {
                newBudgetAmount = budget.SetAmount.Value;
            }
            return newBudgetAmount;
        }
    }
}
