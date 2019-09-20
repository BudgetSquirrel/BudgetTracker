using BudgetTracker.Business.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;

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
                Notes = message.Notes,
                BudgetId = message.BudgetId
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

        public static List<TransactionMessage> Convert(List<Transaction> transactions)
        {
            return transactions.Select(t => Convert(t)).ToList();
        }
    }
}
