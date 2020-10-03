using System;
using System.Collections.Generic;
using System.Linq;
using BudgetSquirrel.Business.Tracking;

namespace BudgetSquirrel.Business.BudgetPlanning
{
    // TODO: Move to roo BudgetSquirrel.Business namespace
    public class Budget
    {
        /// <summary>
        /// Unique numeric identifier for this <see cref="Budget" />.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Allows the user to calculate the Set amount based on the Parent
        /// budgets Set amount. This will be used on creation time and update
        /// time to calculate the new value of this budgets SetAmount based on
        /// it's parent Budget's SetAmount.
        /// </summary>
        public double? PercentAmount { get; private set; }

        /// <summary>
        /// The amount of money that is available in this budget. If this is
        /// null, then it is assumed that this has sub-budgets. This budget then
        /// can be assumed to have a calculated balance of the sum of all of
        /// it's direct sub-balances (which may also have calculated balances).
        /// </summary>
        public decimal SetAmount { get; private set; }

        public Guid FundId { get; private set; }

        public Fund Fund { get; set; }

        public Guid BudgetPeriodId { get; set; }

        public BudgetPeriod BudgetPeriod { get; set; }

        public IEnumerable<Budget> SubBudgets =>
            this.Fund.SubFunds.Select(f =>
                f.GetHistoricalBudgetForPeriod(this.BudgetPeriod));
        
        public Budget ParentBudget =>
            this.Fund.ParentFund.GetHistoricalBudgetForPeriod(
                this.BudgetPeriod);

        /// <summary>
        /// The date in which the user finalized their budget 
        /// unable to edit any values until the edit period comes available.
        /// </summary>
        public DateTime? DateFinalizedTo { get; private set; }

        public bool IsPercentBasedBudget
        {
            get
            {
                return this.PercentAmount != null;
            }
        }

        public bool IsFullyAllocated
        {
            get
            {
                if (this.SetAmount != this.SubBudgetTotalPlannedAmount && this.SubBudgets.Count() != 0)
                    return false;

                foreach (Budget subBudget in this.SubBudgets)
                    if (!subBudget.IsFullyAllocated)
                        return false;

                return true;
            }
        }
    
        public decimal SubBudgetTotalPlannedAmount => this.SubBudgets.Sum(b => b.SetAmount);

        private Budget() {}

        public Budget(Fund fund, BudgetPeriod period, decimal setAmount = 0)
        {
            this.Fund = fund;
            this.BudgetPeriod = period;
            this.SetAmount = setAmount;
        }

        public Budget(Fund fund, Guid periodId, decimal setAmount = 0)
        {
            this.Fund = fund;
            this.BudgetPeriodId = periodId;
            this.SetAmount = setAmount;
        }

        public void SetFinalizedDate()
        {
            this.DateFinalizedTo = DateTime.Now;
        }

        public void SetPercentAmount(double percent)
        {
            if (percent < 0 || percent > 1)
                throw new InvalidOperationException("Can only set percent amount of budget to a number between 0 and 1 inclusive.");
            PercentAmount = percent;
            this.SetAmount = ((decimal)percent) * this.ParentBudget.SetAmount;
        }

        public void SetFixedAmount(decimal amount)
        {
            if (amount < 0)
                throw new InvalidOperationException("Fixed amount of budget must not be less than 0.");
            PercentAmount = null;
            SetAmount = amount;
        }
    }
}
