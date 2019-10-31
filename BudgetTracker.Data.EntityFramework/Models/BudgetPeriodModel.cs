using BudgetTracker.Business.BudgetPeriods;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetTracker.Data.EntityFramework.Models
{
    public class BudgetPeriodModel
    {
        public BudgetPeriodModel() {}

        public BudgetPeriodModel(BudgetPeriod domain)
        {
            Id = domain.Id;
            StartDate = domain.StartDate;
            EndDate = domain.EndDate;
            RootBudgetId = domain.RootBudget?.Id ?? domain.RootBudgetId;
        }

        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid RootBudgetId { get; set; }
        [ForeignKey("RootBudgetId")]
        public BudgetModel RootBudget { get; set; }

        public BudgetPeriod ToDomain()
        {
            BudgetPeriod period = new BudgetPeriod()
            {
                Id = Id,
                StartDate = StartDate,
                EndDate = EndDate,
                RootBudgetId = RootBudgetId
            };
            return period;
        }
    }
}
