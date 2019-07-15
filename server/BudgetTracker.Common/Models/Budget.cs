using BudgetTracker.Common.Models.BudgetDurations;
﻿using System;
using System.Collections.Generic;

namespace BudgetTracker.Common.Models
{
    public class Budget
    {
        /// <summary>
        /// Unique numeric identifier for this <see cref="Budget" />.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// English, user friendly identifier for this <see cref="Budget" />.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The amount of money that is available in this budget. If this is
        /// null, then it is assumed that this has sub-budgets. This budget then
        /// can be assumed to have a calculated balance of the sum of all of
        /// it's direct sub-balances (which may also have calculated balances).
        /// </summary>
        public decimal SetAmount { get; set; }

        /// <summary>
        /// The duration the budget will be per cycle in months.
        /// </summary>
        public BudgetDurationBase Duration { get; set; }

        /// <summary>
        /// The last start date of the budget's cycle need to determine when the
        /// current budget will end and the next one will be begin
        /// </summary>
        public DateTime BudgetStart { get; set; }

        /// <summary>
        /// The parent budget's id, if null then this budget is considered the root budget
        /// </summary>
        public Guid? ParentBudgetId { get; set; }

        /// <summary>
        /// The parent budget of which this is a sub-budget.
        /// </summary>
        public Budget ParentBudget { get; set; }

        /// <summary>
        /// The user that owns this budget.
        /// </summary>
        public User Owner { get; set; }

        /// <summary>
        /// Budgets that are children of this budget.
        /// </summary>
        public List<Budget> SubBudgets { get; set; }

        public override string ToString()
        {
            string str = this.Name + " ($" + this.SetAmount.ToString() + ")";
            return str;
        }
    }
}
