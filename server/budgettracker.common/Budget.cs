using System;
using System.Collections.Generic;

namespace budgettracker.common
{
    public class Budget
    {
        /// <summary>
        /// Unique numeric identifier for this <see cref="Budget" />.
        /// </summary>
        public int ID { get; set; }
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
        public double? SetAmount { get; set; }

        /// <summary>
        /// <p>
        /// All <see cref="Budget" /> that are contained within this
        /// <see cref="Budget" />.
        /// </p>
        /// <p>
        /// These should not have a summed <see cref="Budget.SetAmount" />
        /// that is higher than this <see cref="Budget" />'s
        /// <see cref="Budget.SetAmount" />' unless this
        /// <see cref="Budget.SetAmount" /> is null. Then this
        /// <see cref="Budget.SetAmount" /> is calculated from that sum.
        /// </p>
        /// </summary>
        public List<Budget> SubBudgets { get; set; }

        public override string ToString()
        {
            string str = this.Name + " ($" + this.SetAmount.ToString() + ")";
            return str;
        }
    }
}
