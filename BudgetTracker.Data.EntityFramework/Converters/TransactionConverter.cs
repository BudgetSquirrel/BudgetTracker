using BudgetTracker.Business.Transactions;
using BudgetTracker.Data.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetTracker.Data.EntityFramework.Converters
{
    public class TransactionConverter
    {
        public static Transaction Convert(TransactionModel dto)
        {
            Transaction transaction = new Transaction()
            {
                Id = dto.Id,
                VendorName = dto.VendorName,
                Description = dto.Description,
                Amount = dto.Amount,
                DateOfTransaction = dto.DateOfTransaction,
                CheckNumber = dto.CheckNumber,
                Notes = dto.Notes,
                BudgetId = dto.BudgetId
            };
            return transaction;
        }

        public static TransactionModel Convert(Transaction transaction)
        {
            TransactionModel dto = new TransactionModel()
            {
                Id = transaction.Id ?? Guid.NewGuid(),
                VendorName = transaction.VendorName,
                Description = transaction.Description,
                Amount = transaction.Amount,
                DateOfTransaction = transaction.DateOfTransaction,
                CheckNumber = transaction.CheckNumber,
                BudgetId = transaction.Budget.Id,
                Notes = transaction.Notes
            };
            return dto;
        }

        public static IEnumerable<Transaction> Convert(IEnumerable<TransactionModel> transactionDatas)
        {
            return transactionDatas.Select(t => Convert(t));
        }
    }
}
