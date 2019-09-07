using BudgetTracker.Business.Auth;
using BudgetTracker.Business.Budgeting;
using System;

namespace BudgetTracker.Business.Transactions
{
    public class Transaction
    {
        public Guid? Id { get; set; }
        public string VendorName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public string CheckNumber { get; set; }
        public string Notes { get; set; }
        public Budget Budget { get; set; }
        public User Owner { get; set; }
    }
}
