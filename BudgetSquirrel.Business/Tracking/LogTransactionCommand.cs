using System;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.Business.Tracking
{
  public class LogTransactionCommand
  {
    private string vendorName;
    private decimal amount;
    private string description;
    private DateTime date;
    private string checkNumber;
    private string notes;
    private Budget budget;

    public LogTransactionCommand(string vendorName, decimal amount, string description, DateTime date, string checkNumber, string notes, Budget budget)
    {
      this.vendorName = vendorName;
      this.amount = amount;
      this.description = description;
      this.date = date;
      this.checkNumber = checkNumber;
      this.notes = notes;
      this.budget = budget;
    }

    public Transaction Run()
    {
      Transaction transaction = new Transaction(this.vendorName, this.amount, this.description, this.date, this.checkNumber, this.notes, this.budget);
      this.budget.AddToFund(this.amount);
      return transaction;
    }
  }
}