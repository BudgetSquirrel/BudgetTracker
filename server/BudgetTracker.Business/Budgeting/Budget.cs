using BudgetTracker.Business.BudgetPeriods;
using BudgetTracker.Business.Ports.Repositories;
using BudgetTracker.Business.Auth;
﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetTracker.Business.Budgeting
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
        /// Allows the user to calculate the Set amount based on the Parent
        /// budgets Set amount. This will be used on creation time and update
        /// time to calculate the new value of this budgets SetAmount based on
        /// it's parent Budget's SetAmount.
        /// </summary>
        public double? PercentAmount { get; set; }

        /// <summary>
        /// The amount of money that is available in this budget. If this is
        /// null, then it is assumed that this has sub-budgets. This budget then
        /// can be assumed to have a calculated balance of the sum of all of
        /// it's direct sub-balances (which may also have calculated balances).
        /// </summary>
        public decimal? SetAmount { get; set; }

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

        public bool IsPercentBasedBudget
        {
            get
            {
                return this.PercentAmount != null;
            }
        }

        public bool IsRootBudget
        {
            get
            {
                return this.ParentBudgetId == null;
            }
        }

        public decimal CalculateBudgetSetAmount()
        {
            decimal newBudgetAmount = default(decimal);
            if (IsPercentBasedBudget)
            {
                newBudgetAmount = ParentBudget.SetAmount.Value * (decimal) PercentAmount.Value;
            }
            else
            {
                newBudgetAmount = SetAmount.Value;
            }
            return newBudgetAmount;
        }

        /// <summary>
        /// Takes all attributes of otherBudget and mirrors them
        /// in this budget. This is sort of like clone except it
        /// is cloned to an already instantiated budget object.
        /// </summary>
        public void Mirror(Budget otherBudget)
        {
            Id = otherBudget.Id;
            Name = otherBudget.Name;
            PercentAmount = otherBudget.PercentAmount;
            SetAmount = otherBudget.SetAmount;
            Duration = otherBudget.Duration;
            BudgetStart = otherBudget.BudgetStart;
            ParentBudgetId = otherBudget.ParentBudgetId;
            ParentBudget = otherBudget.ParentBudget;
            Owner = otherBudget.Owner;
            SubBudgets = otherBudget.SubBudgets;
        }

        public Budget Clone()
        {
            Budget clone = new Budget();
            clone.Mirror(this);
            return clone;
        }
    }
}
