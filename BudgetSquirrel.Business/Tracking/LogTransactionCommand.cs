using System;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.Business.Tracking
{
  public class LogTransactionCommand
  {
    public Transaction LogTransaction(string vendorName, decimal amount, string description, DateTime date, string checkNumber, string notes, Budget budget)
    {
      Transaction transaction = new Transaction(vendorName, amount, description, date, checkNumber, notes, budget);
      budget.AddToFund(amount);
      return transaction;
    }
  }
}