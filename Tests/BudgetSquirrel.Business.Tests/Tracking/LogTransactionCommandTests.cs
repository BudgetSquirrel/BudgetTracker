using System;
using Bogus;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Business.Tracking;
using Xunit;

namespace BudgetSquirrel.Business.Tests.Tracking
{
    public class LogTransactionCommandTests : IDisposable
    {
        private static Faker _faker = new Faker();
        private BuilderFactoryFixture _builderFactoryFixture;

        public LogTransactionCommandTests()
        {
            _builderFactoryFixture = new BuilderFactoryFixture();
        }
        
        [Fact]
        public void Test_BudgetFundUpdated_WhenTransactionLogged()
        {
            decimal startFund = 35;
            decimal transactionAmount = 43;
            decimal expectedFund = 78;
            Budget budget = _builderFactoryFixture.BudgetBuilder.SetFundBalance(startFund).Build();
            LogTransactionCommand command = new LogTransactionCommand(
                _faker.Company.CompanyName(),
                transactionAmount,
                _faker.Lorem.Sentence(),
                _faker.Date.Past(),
                "",
                _faker.Lorem.Sentence(),
                budget);

            command.Run();

            Assert.Equal(expectedFund, budget.FundBalance);
        }

        public void Dispose()
        {
            _builderFactoryFixture.Dispose();
        }
    }
}