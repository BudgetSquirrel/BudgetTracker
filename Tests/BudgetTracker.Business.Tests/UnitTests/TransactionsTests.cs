using BudgetTracker.Business.Auth;
using BudgetTracker.Business.Budgeting;
using BudgetTracker.Business.Transactions;
using BudgetTracker.Common.Exceptions;
using BudgetTracker.TestUtils.Auth;
using BudgetTracker.TestUtils.Budgeting;
using BudgetTracker.TestUtils.Transactions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BudgetTracker.Business.Tests.UnitTests
{
    public class TransactionsTests : BaseUnitTest
    {
        private BudgetBuilderFactory<Budget> _budgetBuilderFactory;
        private TransactionBuilderFactory _transactionBuilderFactory;
        private UserFactory _userFactory;

        public TransactionsTests()
        {
            _budgetBuilderFactory = GetService<BudgetBuilderFactory<Budget>>();
            _transactionBuilderFactory = GetService<TransactionBuilderFactory>();
            _userFactory = GetService<UserFactory>();
        }

        [Fact]
        public async Task Test_ErrorThrown_When_TransactionAppliedToBudget_And_NotSameOwner()
        {
            User owner1 = _userFactory.NewUser();
            User owner2 = _userFactory.NewUser();
            Budget budget = _budgetBuilderFactory.GetBuilder()
                                                .SetOwner(owner1)
                                                .Build();
            Transaction transaction = _transactionBuilderFactory.GetBuilder()
                                                .SetOwner(owner2)
                                                .SetBudget(budget)
                                                .Build();

            Exception ex = await Assert.ThrowsAsync<ValidationException>(() => budget.ApplyTransaction(transaction, null));
        }

        [Theory]
        [InlineData(300, -57, 243)]
        [InlineData(10, -57, -47)]
        [InlineData(0, -48, -48)]
        [InlineData(0, 0, 0)]
        [InlineData(0, 39, 39)]
        [InlineData(86, 0, 86)]
        [InlineData(337, 57, 394)]
        public async Task Test_FundBalanceUpdated_When_TransactionAppliedToBudget_And_BudgetIsRootBudget(decimal fundStart, decimal transactionAmount, decimal expectedEndFund)
        {
            User owner = _userFactory.NewUser();
            Budget budget = _budgetBuilderFactory.GetBuilder()
                                                .SetOwner(owner)
                                                .SetFundBalance(fundStart)
                                                .Build();
            Transaction transaction = _transactionBuilderFactory.GetBuilder()
                                                .SetOwner(owner)
                                                .SetBudget(budget)
                                                .SetAmount(transactionAmount)
                                                .Build();

            await budget.ApplyTransaction(transaction, null);
            Assert.Equal(budget.FundBalance, expectedEndFund);
        }

        [Theory]
        [InlineData(500, 300, -57, 443)]
        [InlineData(25, 10, -57, -32)]
        [InlineData(70, 0, -48, 22)]
        [InlineData(0, 0, -48, -48)]
        [InlineData(38, 0, 0, 38)]
        [InlineData(0, 0, 0, 0)]
        [InlineData(12, 0, 39, 51)]
        [InlineData(24, 0, -47, -23)]
        [InlineData(89, 86, 0, 89)]
        [InlineData(406 ,337, 57, 463)]
        public async Task Test_ParentFundBalanceUpdated_When_TransactionAppliedToSubBudget(decimal parentFundStart, decimal childFundStart, decimal transactionAmount, decimal expectedParentEndFund)
        {
            User owner = _userFactory.NewUser();
            Budget parent = _budgetBuilderFactory.GetBuilder()
                                                .SetOwner(owner)
                                                .SetFundBalance(parentFundStart)
                                                .Build();
            Budget child = _budgetBuilderFactory.GetBuilder()
                                                .SetOwner(owner)
                                                .SetParentBudget(parent)
                                                .SetFundBalance(childFundStart)
                                                .Build();
            Transaction transaction = _transactionBuilderFactory.GetBuilder()
                                                .SetOwner(owner)
                                                .SetBudget(child)
                                                .SetAmount(transactionAmount)
                                                .Build();

            await child.ApplyTransaction(transaction, null);
            Assert.Equal(parent.FundBalance, expectedParentEndFund);
        }
    }
}
