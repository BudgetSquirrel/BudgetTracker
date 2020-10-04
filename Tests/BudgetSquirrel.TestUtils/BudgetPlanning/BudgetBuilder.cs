using Bogus;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Business.Tracking;
using System;
using System.Collections.Generic;
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
            if (this.parentBudget != null)
            {
                this.fund.ParentFund = this.parentBudget.Fund;
            }

            Budget budget = null;
            budget = new Budget(this.fund, this.budgetPeriod);
            budget.Fund = this.fund;
            this.fund.HistoricalBudgets = new List<Budget>() { budget };
            
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
