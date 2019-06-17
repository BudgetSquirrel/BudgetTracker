using budgettracker.common.Models.BudgetDurations;
﻿using System;
using System.Collections.Generic;

namespace budgettracker.common.Models
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
        public BudgetDurationModel Duration { get; set; }

        /// <summary>
        /// The last start date of the budget's cycle need to determine when the
        /// current budget will end and the next one will be begin
        /// </summary>
        public DateTime BudgetStart { get; set; }

        /// <summary>
        /// The parent budget's id, if null then this budget is considered the root budget
        /// </summary>
        public Guid? ParentBudgetId { get; set; }

        public override string ToString()
        {
            string str = this.Name + " ($" + this.SetAmount.ToString() + ")";
            return str;
        }
    }
}
