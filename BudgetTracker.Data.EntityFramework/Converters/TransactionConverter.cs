using BudgetTracker.Business.Transactions;
using BudgetTracker.Data.EntityFramework.Models;
using System;

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
    }
}