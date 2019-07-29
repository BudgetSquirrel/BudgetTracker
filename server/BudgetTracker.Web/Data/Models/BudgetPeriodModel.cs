using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetTracker.Web.Data.Models
{
    public class BudgetPeriodModel
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid RootBudgetId { get; set; }
        [ForeignKey("RootBudgetId")]
        public BudgetModel RootBudget { get; set; }
    }
}
