using System;
using System.Collections.Generic;
using budgettracker.common;
using budgettracker.common.Models;

namespace budgettracker.business.Serialization.Converters
{
    /// <summary>
    /// Converts a Budget object to a contract that can be serialized into
    /// JSON. This will be used by the API layer.
    /// </summary>
    public class BudgetSerializationConverter
    {
        #region Private Functions
        /// <summary>
        /// Returns a list of serializable budget contracts.
        /// </summary>
        private static List<BudgetSerializationContract> GetSubBudgets(Budget budget)
        {
            List<BudgetSerializationContract> subBudgets = new List<BudgetSerializationContract>();
            if (budget.SubBudgets == null)
            {
                return subBudgets;
            }
            foreach (Budget subBudget in budget.SubBudgets)
            {
                subBudgets.Add(ModelToSerializationContract(subBudget));
            }
            return subBudgets;
        }
        #endregion

        #region Public Interface
        /// <summary>
        /// Converts a <see cref="Budget" /> object to a
        /// <see cref="BudgetSerializationContract" /> so that it can be
        /// serialized.
        /// </summary>
        public static BudgetSerializationContract ModelToSerializationContract(Budget budget)
        {
            BudgetSerializationContract contract = new BudgetSerializationContract()
            {
                ID = budget.Id,
                Name = budget.Name,
                SetAmount = budget.SetAmount,
                ActualBudget = BudgetCalculations.GetCalculatedBudget(budget),
                SubBudgets = GetSubBudgets(budget)
            };
            return contract;
        }
        #endregion

    }
}
