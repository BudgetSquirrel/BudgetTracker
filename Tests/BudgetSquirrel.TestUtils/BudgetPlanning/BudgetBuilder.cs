using Bogus;
using BudgetSquirrel.Business.BudgetPlanning;
using System;

namespace BudgetSquirrel.TestUtils.Budgeting
{
    public class BudgetBuilder : IBudgetBuilder
    {
        private Faker _faker = new Faker();
        private BudgetDurationBuilderProvider _budgetDurationBuilderProvider;

        private Guid Id;

        private Guid _ownerId;

        private string _name;

        private double? _percentAmount;

        private decimal? _setAmount;

        private decimal _fundBalance;

        private IBudgetDurationBuilder _durationBuilder;

        private DateTime _budgetStart;

        public BudgetBuilder(BudgetDurationBuilderProvider budgetDurationBuilderProvider)
        {
            _budgetDurationBuilderProvider = budgetDurationBuilderProvider;
            InitRandomized();
        }

        private void InitRandomized()
        {
            Id = Guid.NewGuid();
            _ownerId = Guid.NewGuid();
            _name = _faker.Lorem.Word();
            _budgetStart = DateTime.Now;

            bool isPercentBased = _faker.Random.Bool();
            if (isPercentBased)
                _percentAmount = _faker.Random.Double();
            else
                _setAmount = _faker.Finance.Amount();
            InitRandomDuration();
        }

        private void InitRandomDuration(bool? isDaySpanDuration=null)
        {
            isDaySpanDuration = isDaySpanDuration ?? _faker.Random.Bool();

            if (isDaySpanDuration == true)
            {
                _durationBuilder = _budgetDurationBuilderProvider.GetBuilder<DaySpanDuration>();
            }
            else
            {
                _durationBuilder = _budgetDurationBuilderProvider.GetBuilder<MonthlyBookEndedDuration>();
            }
        }

        public IBudgetBuilder SetOwner(Guid userId)
        {
            _ownerId = userId;
            return this;
        }

        public IBudgetBuilder SetName(string name)
        {
            _name = name;
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

        public IBudgetBuilder SetFundBalance(decimal fundBalance) {
            _fundBalance = fundBalance;
            return this;
        }

        public IBudgetBuilder SetDurationEndDayOfMonth(int value)
        {
            if (_durationBuilder == null || !(_durationBuilder is MonthlyBookEndedDurationBuilder))
                InitRandomDuration(false);
            ((MonthlyBookEndedDurationBuilder) _durationBuilder).SetDurationEndDayOfMonth(value);
            return this;
        }

        public IBudgetBuilder SetDurationRolloverEndDateOnSmallMonths(bool value)
        {
            if (_durationBuilder == null || !(_durationBuilder is MonthlyBookEndedDurationBuilder))
                InitRandomDuration(false);
            ((MonthlyBookEndedDurationBuilder) _durationBuilder).SetDurationRolloverEndDateOnSmallMonths(value);
            return this;
        }

        public IBudgetBuilder SetDurationNumberDays(int value)
        {
            if (_durationBuilder == null || !(_durationBuilder is DaySpanDurationBuilder))
                InitRandomDuration(true);
            ((DaySpanDurationBuilder) _durationBuilder).SetNumberDays(value);
            return this;
        }

        public Budget Build()
        {
            BudgetDurationBase budgetDuration = _durationBuilder.Build();

            Budget budget = new Budget(
                Guid.NewGuid(),
                _name,
                _fundBalance,
                budgetDuration,
                _budgetStart,
                _ownerId
            );
            if (_percentAmount != null)
                budget.SetPercentAmount(_percentAmount.Value);
            else
                budget.SetFixedAmount(_setAmount.Value);
            
            return budget;
        }
    }
}
