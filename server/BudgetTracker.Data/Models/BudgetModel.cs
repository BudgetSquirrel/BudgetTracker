using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetTracker.Data.Models
{
    public class BudgetModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal SetAmount { get; set; }
        public DateTime BudgetStart { get; set; }
        public DateTime CreatedDate { get; set; }

        public Guid OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public UserModel Owner { get; set; }

        public Guid? ParentBudgetId { get; set; }
        [ForeignKey("ParentBudgetId")]
        public BudgetModel ParentBudget { get; set; }

        public Guid DurationId { get; set; }
        [ForeignKey("DurationId")]
        public BudgetDurationModel Duration { get; set; }
    }
}
