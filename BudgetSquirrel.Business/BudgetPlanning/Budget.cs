using System;

namespace BudgetSquirrel.Business.BudgetPlanning
{
    public class Budget
    {
        /// <summary>
        /// Unique numeric identifier for this <see cref="Budget" />.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// English, user friendly identifier for this <see cref="Budget" />.
        /// </summary>
        public string Name { get; private set; }

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
        public decimal? SetAmount { get; private set; }

        /// <summary>
        /// The amount that is currently available in the fund attached to this
        /// budget for the budget planner to spend. Each time a transaction is
        /// applied to this budget, this balance will be modified to reflect that
        /// transaction.
        /// It is important to note that this number is agnostic of the budget
        /// amounts (PercentAmount/SetAmount). Those are planned amount that the
        /// user will put into the budget fund every budget period. This is the
        /// amount that stores that planned amount along with any rollover from
        /// previous months.
        /// For example, a user may put $50 into this budget every budget period,
        /// but after 3 budget periods, if they don't spend anything, this fund
        /// balance will have a value of $150 (3 budget periods worth of saving
        /// $50 each period). If they then go and log a transaction against this
        /// budget of $37, this fund balance will then only be $113 (150 - 137).
        /// </summary>
        public decimal FundBalance { get; private set; }

        /// <summary>
        /// The duration the budget will be per cycle in months.
        /// </summary>
        public BudgetDurationBase Duration { get; set; }

        /// <summary>
        /// The last start date of the budget's cycle need to determine when the
        /// current budget will end and the next one will be begin
        /// </summary>
        public DateTime BudgetStart { get; private set; }

        public bool IsPercentBasedBudget
        {
            get
            {
                return this.PercentAmount != null;
            }
        }

        public Budget(string name, decimal fundBalance,
            BudgetDurationBase duration, DateTime budgetStart)
        {
            Name = name;
            FundBalance = fundBalance;
            Duration = duration;
            BudgetStart = budgetStart;
            SetAmount = 0;
        }

        public Budget(Guid id, string name, decimal fundBalance,
            BudgetDurationBase duration, DateTime budgetStart)
        {
            Id = id;
            Name = name;
            FundBalance = fundBalance;
            Duration = duration;
            BudgetStart = budgetStart;
            SetAmount = 0;
        }

        public void SetPercentAmount(double percent)
        {
            if (percent < 0 || percent > 1)
                throw new InvalidOperationException("Can only set percent amount of budget to a number between 0 and 1 inclusive.");
            SetAmount = null;
            PercentAmount = percent;
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
