using System;
using System.Collections.Generic;
using System.Linq;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Business.Tracking;

namespace BudgetSquirrel.Business
{
    // TODO: Move to roo BudgetSquirrel.Business namespace
    public class Fund
    {
        public Guid Id { get; set; }

        /// <summary>
        /// English, user friendly identifier for this <see cref="Budget" />.
        /// </summary>
        public string Name { get; set; }

        public Guid DurationId { get; private set; }

        public Guid UserId { get; private set; }

        public User User { get; set; }

        /// <summary>
        /// The duration the budget will be per cycle in months.
        /// </summary>
        public BudgetDurationBase Duration { get; set; }

        public IEnumerable<Budget> HistoricalBudgets { get; set; }

        public Budget GetHistoricalBudgetForPeriod(BudgetPeriod period)
        {
            return this.HistoricalBudgets.SingleOrDefault(b => b.BudgetPeriod.StartDate == period.StartDate);
        }

        public Guid? ParentFundId { get; set; }

        public Fund ParentFund { get; set; }

        public IEnumerable<Fund> SubFunds { get; set; }

        public Budget CurrentBudget 
        { 
            get
            {
                return this.HistoricalBudgets.Where(b => b.BudgetPeriod.StartDate < DateTime.Now && b.BudgetPeriod.EndDate > DateTime.Now).SingleOrDefault();
            }
        }
        
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

        private Fund() {}

        public Fund(string name, decimal fundBalance,
            BudgetDurationBase duration,
            Guid userId)
        {
            this.Name = name;
            this.FundBalance = fundBalance;
            this.Duration = duration;
            this.UserId = userId;
            this.SubFunds = new List<Fund>();
        }

        public Fund(Guid id, string name, decimal fundBalance,
            BudgetDurationBase duration,
            Guid userId)
        {
            this.Id = id;
            this.Name = name;
            this.FundBalance = fundBalance;
            this.Duration = duration;
            this.UserId = userId;
            this.SubFunds = new List<Fund>();
        }

        public Fund(Fund parentFund, string name, decimal fundBalance)
        {
            this.ParentFundId = parentFund.Id;
            this.DurationId = parentFund.DurationId;
            this.UserId = parentFund.UserId;
            this.Name = name;
            this.FundBalance = fundBalance;
            this.SubFunds = new List<Fund>();
        }

        public void SetOwner(Guid userId)
        {
            if (this.UserId != default(Guid))
            {
                throw new InvalidOperationException("This budget already has an owner.");
            }
            this.UserId = userId;
        }

        public bool IsOwnedBy(User user)
        {
            return this.UserId == user.Id;
        }

        public void ApplyTransaction(Transaction transaction)
        {
            this.FundBalance += transaction.Amount;
            if (this.ParentFundId.HasValue)
            {
                this.ParentFund.ApplyTransaction(transaction);
            }
        }
    }
}
