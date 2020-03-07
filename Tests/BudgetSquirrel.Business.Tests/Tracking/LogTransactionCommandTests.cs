using System;
using BudgetSquirrel.Business.BudgetPlanning;
using Xunit;

namespace BudgetSquirrel.Business.Tests.Tracking
{
    public class LogTransactionCommandTests : IDisposable
    {
        private BuilderFactoryFixture _builderFactoryFixture;

        public LogTransactionCommandTests()
        {
            _builderFactoryFixture = new BuilderFactoryFixture();
        }
        
        [Fact]
        public void Test_StuffInLogTransactionCommand()
        {
            Assert.True(false);
        }

        [Fact]
        public void Test_BudgetFundUpdated_WhenTransactionLogged()
        {
            Budget budget = _builderFactoryFixture.BudgetBuilder.SetFundBalance(35).Build();
        }

        public void Dispose()
        {
            _builderFactoryFixture.Dispose();
        }
    }
}