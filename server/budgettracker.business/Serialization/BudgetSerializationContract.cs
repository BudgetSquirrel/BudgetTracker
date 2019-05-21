using System;
using System.Collections.Generic;
using budgettracker.common;

namespace budgettracker.business.Serialization
{
    /// <summary>
    /// A contract that represents a <see cref="Budget" /> object. This can
    /// easily be converted to a string such as JSON or XML.
    /// </summary>
    public class BudgetSerializationContract
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
        /// can be assumed to have a calculated budget of the sum of all of
        /// it's direct sub-budgets (which may also have calculated budgets).
        /// </summary>
        public decimal? SetAmount { get; set; }
        /// <summary>
        /// <p>
        /// The amount of money that is available in this budget. This factors
        /// Sub-categories.
        /// </p>
        /// <p>
        /// If this budgets <see cref="Budget.SetAmount" /> is not null, then
        /// this will simply be the value of that. Otherwise, this will be
        /// calculated from the children in <see cref="Budget.SubBudgets" />.
        /// </p>
        /// </summary>
        public double ActualBudget { get; set; }

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
        public List<BudgetSerializationContract> SubBudgets { get; set; }
    }
}
