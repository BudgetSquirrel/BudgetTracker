using System;

namespace BudgetTracker.Data.Models
{
    public class BudgetPeriodModel
    {
        public Guid BudgetId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}