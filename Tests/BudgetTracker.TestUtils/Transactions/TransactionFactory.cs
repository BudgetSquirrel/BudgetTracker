using Bogus;
using BudgetTracker.Business.Budgeting;
using BudgetTracker.Business.Transactions;
using BudgetTracker.Business.Ports.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetTracker.TestUtils.Transactions
{
    public class TransactionFactory
    {
        private TransactionBuilderFactory _transactionBuilderFactory;
        private Faker _faker;

        public TransactionFactory(TransactionBuilderFactory transactionBuilderFactory)
        {
            _faker = new Faker();
            _transactionBuilderFactory = transactionBuilderFactory;
        }

        public async Task<Transaction> CreateTransactionFor(Budget budget,
            ITransactionRepository transactionRepository,
            IBudgetRepository budgetRepository, decimal? amount=null)
        {
            amount = amount ?? (_faker.Finance.Amount(max: budget.SetAmount.Value) * (decimal)-1.0);
            Transaction toCreate = _transactionBuilderFactory.GetBuilder()
                                                            .SetAmount(amount.Value)
                                                            .SetOwner(budget.Owner)
                                                            .SetBudget(budget)
                                                            .Build();
            Transaction created = await transactionRepository.CreateTransaction(toCreate);
            budget.FundBalance += amount.Value;
            Budget updatedBudget = await budgetRepository.UpdateBudget(budget);
            budget.Mirror(updatedBudget);
            await budgetRepository.LoadSubBudgets(budget, true);
            return created;
        }
    }
}
