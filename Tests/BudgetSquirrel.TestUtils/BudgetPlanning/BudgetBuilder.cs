using Bogus;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Business.Tracking;
using System;
using System.Linq;

namespace BudgetSquirrel.TestUtils.Budgeting
{
    public class BudgetBuilder : IBudgetBuilder
    {
        private Faker _faker = new Faker();
        private IFundBuilder _fundBuilder;

        private Fund fund;

        private Budget parentBudget;

        private BudgetPeriod budgetPeriod;

        private Guid Id;

        private double? _percentAmount;

        private decimal? _setAmount;

        public BudgetBuilder(IFundBuilder fundBuilder)
        {
            _fundBuilder = fundBuilder;
            InitRandomized();
        }

        private void InitRandomized()
        {
            Id = Guid.NewGuid();

            bool isPercentBased = _faker.Random.Bool();
            if (isPercentBased)
                _percentAmount = _faker.Random.Double();
            else
                _setAmount = _faker.Finance.Amount();

            this.fund = this._fundBuilder.Build();

            DateTime now = DateTime.Now;
            this.budgetPeriod = new BudgetPeriod(now, this.fund.Duration.GetEndDateFromStartDate(now));
        }

        public IBudgetBuilder SetFund(Func<IFundBuilder, IFundBuilder> fundOptions)
        {
            this.fund = fundOptions(this._fundBuilder).Build();
            return this;
        }

        public IBudgetBuilder SetPercentAmount(double? percentAmount)
        {
            _setAmount = null;
            _percentAmount = percentAmount;
            return this;
        }

        public IBudgetBuilder SetFixedAmount(decimal? setAmount) {
            _percentAmount = null;
            _setAmount = setAmount;
            return this;
        }

        public Budget Build()
        {
            if (this.fund.ParentFund != null || this.parentBudget != null)
            {
                this.fund.ParentFund.HistoricalBudgets = this.fund.ParentFund.HistoricalBudgets.Append(this.parentBudget);
                this.parentBudget.Fund = this.fund.ParentFund;
            }
            Budget budget = null;
            budget = new Budget(this.fund, this.budgetPeriod);
            if (_percentAmount.HasValue)
                budget.SetPercentAmount(_percentAmount.Value);
            else
                budget.SetFixedAmount(_setAmount.Value);
            
            return budget;
        }

        public IBudgetBuilder SetParentBudget(Budget budget)
        {
            this.parentBudget = budget;
            this.parentBudget.BudgetPeriod = this.budgetPeriod;
            return this;
        }
  }
}
