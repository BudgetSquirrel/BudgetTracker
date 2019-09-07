using BudgetTracker.Business.Budgeting;
using System;

namespace BudgetTracker.Business.Transactions
{
    public class TransactionMessage
    {
        public Guid? Id { get; set; }
        public string VendorName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public string CheckNumber { get; set; }
        public string Notes { get; set; }
        public Guid BudgetId { get; set; }
    }
}
