using System;
using System.Collections.Generic;
using budgettracker.common;
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
                ID=1,
                Name="Kirkpatricks Budget",
                SetAmount=60000,
                SubBudgets = new List<Budget>() {
                    new Budget() {
                        ID=2,
                        Name="Rent",
                        SetAmount=1048.0,
                        SubBudgets = new List<Budget>() {

                        }
                    },
                    new Budget() {
                        ID=3,
                        Name="Food",
                        SetAmount=350.0,
                        SubBudgets = new List<Budget>() {

                        }
                    },
                    new Budget() {
                        ID=4,
                        Name="Electric",
                        SetAmount=75.0,
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
