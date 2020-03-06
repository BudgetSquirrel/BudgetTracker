using System;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.Business.Tracking
{
  public class Transaction
  {
    public Guid Id { get; private set; }
    public string VendorName { get; private set; }
    public string Description { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime DateOfTransaction { get; private set; }
    public string CheckNumber { get; private set; }
    public string Notes { get; private set; }
    public Budget Budget { get; private set; }

    public Transaction(string vendorName, decimal amount, string description, DateTime date, string checkNumber, string notes, Budget forBudget)
    {
      VendorName = vendorName;
      Description = description;
      DateOfTransaction = date;
      CheckNumber = checkNumber;
      Notes = notes;
      Budget = forBudget;
    }

    public Transaction(Guid id, string vendorName, decimal amount,  string description, DateTime date, string checkNumber, string notes, Budget forBudget)
      : this(vendorName, amount, description, date, checkNumber, notes, forBudget)
    {
      Id = id;
    }

    public void SetAmount(decimal amount)
    {
      this.Amount = amount;
    }
  }
}