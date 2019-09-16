using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetTracker.Data.EntityFramework.Models
{
    public class TransactionModel
    {
        public Guid Id { get; set; }
        public string VendorName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public string CheckNumber { get; set; }
        [MaxLength(500)]
        public string Notes { get; set; }

        public Guid BudgetId { get; set; }
        [ForeignKey("BudgetId")]
        public BudgetModel Budget { get; set; }
    }
}
