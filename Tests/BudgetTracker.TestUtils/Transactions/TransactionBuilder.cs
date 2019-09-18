using Bogus;
using BudgetTracker.Business.Auth;
using BudgetTracker.Business.Budgeting;
using BudgetTracker.Business.Transactions;
using System;

namespace BudgetTracker.TestUtils.Transactions
{
    public class TransactionBuilder
    {
        private Faker _faker = new Faker();

        private Transaction _transactionBuild;

        public TransactionBuilder()
        {
            InitDefaults();
        }

        private void InitDefaults()
        {
            _transactionBuild = new Transaction()
            {
                VendorName = _faker.Company.CompanyName(),
                Description = _faker.Commerce.Product(),
                Amount = _faker.Finance.Amount(),
                DateOfTransaction = DateTime.Now,
                CheckNumber = _faker.Random.Number().ToString(),
                Notes = _faker.Lorem.Sentences()
            };
        }

        public TransactionBuilder SetAmount(decimal amount)
        {
            _transactionBuild.Amount = amount;
            return this;
        }

        public TransactionBuilder SetDateOfTransaction(DateTime date)
        {
            _transactionBuild.DateOfTransaction = date;
            return this;
        }

        public TransactionBuilder SetBudget(Budget budget)
        {
            _transactionBuild.Budget = budget;
            return this;
        }

        public TransactionBuilder SetOwner(User owner)
        {
            _transactionBuild.Owner = owner;
            return this;
        }

        public Transaction Build()
        {
            return _transactionBuild;
        }
    }
}
