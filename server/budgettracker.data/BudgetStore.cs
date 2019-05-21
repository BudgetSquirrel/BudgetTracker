using System;
using System.Collections.Generic;
using budgettracker.common.Models;

namespace budgettracker.data
{
    public class BudgetStore {
        #region Public Interface
        /// <Summary>
        /// Gets the budget.
        /// </Summary>
        public static Budget Get()
        {
            Budget budget = new Budget() {
                Id=1,
                Name="Kirkpatricks Budget",
                SetAmount=60000,
                SubBudgets = new List<Budget>() {
                    new Budget() {
                        Id=2,
                        Name="Rent",
                        SetAmount=1048.0M,
                        SubBudgets = new List<Budget>() {

                        }
                    },
                    new Budget() {
                        Id=3,
                        Name="Food",
                        SetAmount=350.0M,
                        SubBudgets = new List<Budget>() {

                        }
                    },
                    new Budget() {
                        Id=4,
                        Name="Electric",
                        SetAmount=75.0M,
                        SubBudgets = new List<Budget>() {

                        }
                    }
                }
            };
            return budget;
        }
        #endregion
    }
}
