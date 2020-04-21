using System;

namespace BudgetSquirrel.Business.Tracking
{
    public class BudgetPeriod
    {
        public Guid Id { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public BudgetPeriod(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public BudgetPeriod(Guid id, DateTime startDate, DateTime endDate)
            : this(startDate, endDate)
        {
            Id = id;
        }
    }
}