// using Bogus;
// using System;

// namespace BudgetSquirrel.TestUtils.Transactions
// {
//     public class TransactionMessageBuilder
//     {
//         private Faker _faker = new Faker();

//         private TransactionMessage _transactionBuild;

//         public TransactionMessageBuilder()
//         {
//             InitDefaults();
//         }

//         private void InitDefaults()
//         {
//             _transactionBuild = new TransactionMessage()
//             {
//                 VendorName = _faker.Company.CompanyName(),
//                 Description = _faker.Commerce.Product(),
//                 Amount = _faker.Finance.Amount(),
//                 DateOfTransaction = DateTime.Now,
//                 CheckNumber = _faker.Random.Number().ToString(),
//                 Notes = _faker.Lorem.Sentences()
//             };
//         }

//         public TransactionMessageBuilder SetAmount(decimal amount)
//         {
//             _transactionBuild.Amount = amount;
//             return this;
//         }

//         public TransactionMessageBuilder SetDateOfTransaction(DateTime date)
//         {
//             _transactionBuild.DateOfTransaction = date;
//             return this;
//         }

//         public TransactionMessageBuilder SetBudgetId(Guid budgetId)
//         {
//             _transactionBuild.BudgetId = budgetId;
//             return this;
//         }

//         public TransactionMessage Build()
//         {
//             return _transactionBuild;
//         }
//     }
// }
