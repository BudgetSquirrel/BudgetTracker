using budgettracker.common.Models;

namespace budgettracker.business
{
    public class BudgetCalculations
    {
        #region Private Functions
        /// <summary>
        /// <p>
        /// The total amount of money that is budgeted according to the
        /// sub-budgets in this budget. This is done by getting all calculated
        /// budgets for each direct child sub budget of this budget, and summing
        /// the budget ammounts up. That sum will be the amount of this budget.
        /// Of course, this is recursive in order for that to happen.
        /// </p>
        /// </summary>
        private static double GetSubBudgetsTotalBudgetAmount(Budget budget)
        {
            double budgetAmount = 0.0;
            foreach (Budget subBudget in budget.SubBudgets)
            {
                budgetAmount += GetCalculatedBudget(subBudget);
            }
            return budgetAmount;
        }
        #endregion


        #region Public Interface
        /// <summary>
        /// <p>
        /// Calculates the total amount budgeted in this budget item.
        /// </p>
        /// <p>
        /// If the budget's <see cref="Budget.SetAmount" /> is not null, then
        /// this will return that amount.
        /// </p>
        /// <p>
        /// Otherwise, if this has sub-budgets, then this will sum up the
        /// calculated budget amounts for all sub-budgets and return that sum.
        /// </p>
        /// <p>
        /// Otherwise, this will simply return 0.
        /// </p>
        /// </summary>
        public static double GetCalculatedBudget(Budget budget)
        {
            double calculatedBudget = 0.0;
            if (budget.SetAmount != null)
            {
                calculatedBudget = (double) budget.SetAmount;
            }
            else if (budget.SubBudgets.Count > 0)
            {
                calculatedBudget = GetSubBudgetsTotalBudgetAmount(budget);
            }
            // else calculatedBudget = 0.0; But this is unnecessary code.
            return calculatedBudget;
        }
        #endregion

    }
}
