using BudgetTracker.Business.Auth;
using BudgetTracker.Common;
using BudgetTracker.Common.Exceptions;
using BudgetTracker.Business.Ports.Exceptions;
using BudgetTracker.Business.Ports.Repositories;
using System;
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
                if (user.Id != budgetCreateObject.ParentBudget.Owner.Id)
                {
                    throw new AuthorizationException("This user does not have access to the specified parent budget.");
                }
                budgetCreateObject.Duration = budgetCreateObject.ParentBudget.Duration;
            }

            budgetCreateObject.SetAmount = budgetCreateObject.CalculateBudgetSetAmount();
            budgetCreateObject = await budgetRepository.CreateBudget(budgetCreateObject);

            return budgetCreateObject;
        }
    }
}
