using System;
using Bogus;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.TestUtils.Budgeting
{
    public class MonthlyBookEndedDurationBuilder : IBudgetDurationBuilder
    {
        private Faker _faker = new Faker();

        private int _endDayOfMonth;

        private bool _rolloverEndDateOnSmallMonths;

        public MonthlyBookEndedDurationBuilder()
        {
            InitRandom();
        }

        private void InitRandom()
        {
            _endDayOfMonth = _faker.Random.Number(29,31);
            _rolloverEndDateOnSmallMonths = _faker.Random.Bool();
        }

        public MonthlyBookEndedDurationBuilder SetDurationEndDayOfMonth(int? value)
        {
            _endDayOfMonth = value.Value;
            return this;
        }

        public MonthlyBookEndedDurationBuilder SetDurationRolloverEndDateOnSmallMonths(bool? value)
        {
            _rolloverEndDateOnSmallMonths = value.Value;
            return this;
        }
        
        public BudgetDurationBase Build()
        {
            return new MonthlyBookEndedDuration(Guid.NewGuid(), _endDayOfMonth, _rolloverEndDateOnSmallMonths);
        }
    }
}