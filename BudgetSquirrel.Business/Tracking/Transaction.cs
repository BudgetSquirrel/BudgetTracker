using System;

namespace BudgetSquirrel.Business.Tracking
{
  public class Transaction
  {
    public Guid Id { get; set; }
    public string Summary { get; set; }
    public string Vendor { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string CheckNumber { get; set; }
    public string Notes { get; set; }

    public Guid FundId { get; set; }
    public Fund Fund { get; set; }

    public Transaction() {}

    public Transaction(
      string summary,
      string vendor,
      DateTime date,
      decimal amount,
      string checkNumber,
      string notes,
      Guid fundId)
    {
      this.Summary = summary;
      this.Vendor = vendor;
      this.Date = date;
      this.Amount = amount;
      this.CheckNumber = checkNumber;
      this.Notes = notes;
      this.FundId = fundId;
    }
  }
}