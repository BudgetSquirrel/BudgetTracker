using BudgetTracker.Business.Transactions;
using System;

namespace BudgetTracker.Business.Converters
{
    public class TransactionConverters
    {
        public static Transaction Convert(TransactionMessage message)
        {
            Transaction transaction = new Transaction()
            {
                Id = message.Id,
                VendorName = message.VendorName,
                Description = message.Description,
                Amount = message.Amount,
                DateOfTransaction = message.DateOfTransaction,
                CheckNumber = message.CheckNumber,
                Notes = message.Notes
            };
            return transaction;
        }

        public static TransactionMessage Convert(Transaction transaction)
        {
            TransactionMessage message = new TransactionMessage()
            {
                Id = transaction.Id,
                VendorName = transaction.VendorName,
                Description = transaction.Description,
                Amount = transaction.Amount,
                DateOfTransaction = transaction.DateOfTransaction,
                CheckNumber = transaction.CheckNumber,
                Notes = transaction.Notes,
                BudgetId = transaction.Budget.Id
            };
            return message;
        }
    }
}
