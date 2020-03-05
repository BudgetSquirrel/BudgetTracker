using System;
using Bogus;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.TestUtils.Budgeting
{
    public class MonthlyBookEndedDurationBuilder : IBudgetDurationBuilder
    {
        private Faker _faker = new Faker();
        
        private int _startDayOfMonth;

        private int _endDayOfMonth;

        private bool _rolloverStartDateOnSmallMonths;

        private bool _rolloverEndDateOnSmallMonths;

        public MonthlyBookEndedDurationBuilder()
        {
            InitRandom();
        }

        private void InitRandom()
        {
            int startDayOfMonth = _faker.Random.Number(1,10);
            int daysSpanned = _faker.Random.Number(29,31);
                
            _startDayOfMonth = startDayOfMonth;
            _endDayOfMonth = startDayOfMonth + daysSpanned;
            _rolloverStartDateOnSmallMonths = _faker.Random.Bool();
            _rolloverEndDateOnSmallMonths = _faker.Random.Bool();
        }

        public MonthlyBookEndedDurationBuilder SetDurationStartDayOfMonth(int? value)
        {
            _startDayOfMonth = value.Value;
            return this;
        }

        public MonthlyBookEndedDurationBuilder SetDurationEndDayOfMonth(int? value)
        {
            _endDayOfMonth = value.Value;
            return this;
        }

        public MonthlyBookEndedDurationBuilder SetDurationRolloverStartDateOnSmallMonths(bool? value)
        {
            _rolloverStartDateOnSmallMonths = value.Value;
            return this;
        }

        public MonthlyBookEndedDurationBuilder SetDurationRolloverEndDateOnSmallMonths(bool? value)
        {
            _rolloverEndDateOnSmallMonths = value.Value;
            return this;
        }
        
        public BudgetDurationBase Build()
        {
            return new MonthlyBookEndedDuration(Guid.NewGuid(), _startDayOfMonth, _endDayOfMonth, _rolloverStartDateOnSmallMonths, _rolloverEndDateOnSmallMonths);
        }
    }
}