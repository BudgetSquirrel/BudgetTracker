using System;
using Bogus;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.TestUtils.Budgeting
{
    public class DaySpanDurationBuilder : IBudgetDurationBuilder
    {
        private Faker _faker = new Faker();
        
        private int _numberDays;

        public DaySpanDurationBuilder()
        {

        }

        private void InitRandom()
        {
            _numberDays = _faker.Random.Number(29,31);
        }

        public DaySpanDurationBuilder SetNumberDays(int numberDays)
        {
            _numberDays = numberDays;
            return this;
        }

        public BudgetDurationBase Build()
        {
            return new DaySpanDuration(Guid.NewGuid(), _numberDays);
        }
    }
}