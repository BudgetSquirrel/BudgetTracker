// using Bogus;
// using System.Threading.Tasks;

// namespace BudgetSquirrel.TestUtils.Transactions
// {
//     public class TransactionFactory
//     {
//         private TransactionBuilderFactory _transactionBuilderFactory;
//         private Faker _faker;

//         public TransactionFactory(TransactionBuilderFactory transactionBuilderFactory)
//         {
//             _faker = new Faker();
//             _transactionBuilderFactory = transactionBuilderFactory;
//         }

//         public async Task<Transaction> CreateTransactionFor(Budget budget,
//             ITransactionRepository transactionRepository,
//             IBudgetRepository budgetRepository, decimal? amount=null)
//         {
//             amount = amount ?? (_faker.Finance.Amount(max: budget.SetAmount.Value) * (decimal)-1.0);
//             Transaction toCreate = _transactionBuilderFactory.GetBuilder()
//                                                             .SetAmount(amount.Value)
//                                                             .SetOwner(budget.Owner)
//                                                             .SetBudget(budget)
//                                                             .Build();
//             Transaction created = await transactionRepository.CreateTransaction(toCreate);
//             budget.FundBalance += amount.Value;
//             Budget updatedBudget = await budgetRepository.UpdateBudget(budget);
//             budget.Mirror(updatedBudget);
//             await budgetRepository.LoadSubBudgets(budget, true);
//             return created;
//         }
//     }
// }
