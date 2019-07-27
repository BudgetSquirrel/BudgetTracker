using BudgetTracker.Common.Models;

namespace BudgetTracker.Business.Budgeting
{
    public class DerivedBudgetAttributes
    {
        public static bool IsBudgetPercentBasedBudget(Budget budget)
        {
            return budget.PercentAmount != null;
        }

        public static decimal CalculateBudgetSetAmount(Budget budget)
        {
            decimal newBudgetAmount = default(decimal);
            if (IsBudgetPercentBasedBudget(budget))
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
