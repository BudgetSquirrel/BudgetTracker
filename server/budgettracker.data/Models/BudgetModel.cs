using System;

namespace budgettracker.data.Models
{
    public class BudgetModel 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal SetAmount { get; set; }
        public int Duration { get; set; }
        public DateTime BudgetStart { get; set; }
        public Guid ParentBudgetId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}