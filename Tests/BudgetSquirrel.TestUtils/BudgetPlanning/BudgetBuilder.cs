using Bogus;
using BudgetSquirrel.Business;
using BudgetSquirrel.Business.BudgetPlanning;
using System;
using System.Collections.Generic;

namespace BudgetSquirrel.TestUtils.Budgeting
{
  public class BudgetBuilder : IBudgetBuilder
    {
        private Faker _faker = new Faker();
        private IFundBuilder _fundBuilder;

        private Fund fund;

        private Budget parentBudget;

        private BudgetPeriod budgetPeriod;

        private Guid _id;

        private double? _percentAmount;

        private decimal? _setAmount;

        public BudgetBuilder(IFundBuilder fundBuilder)
        {
            _fundBuilder = fundBuilder;
            InitRandomized();
        }

        private void InitRandomized()
        {
            _id = Guid.NewGuid();

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

        public IBudgetBuilder SetBudgetPeriod(BudgetPeriod budgetPeriod)
        {
            this.budgetPeriod = budgetPeriod;
            return this;
        }

        public Budget Build()
        {
            Budget budget = null;
            budget = new Budget(this._id, this.fund, this.budgetPeriod);
            budget.BudgetPeriodId = this.budgetPeriod.Id;
            budget.FundId = this.fund.Id;
            this.fund.HistoricalBudgets = new List<Budget>() { budget };

            if (this.parentBudget != null)
            {
                budget.Fund.ParentFund = this.parentBudget.Fund;
            }

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
