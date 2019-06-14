using budgettracker.business.Api.Contracts;
using System;

namespace budgettracker.business.Api.Contracts.BudgetApi.BudgetDurations
{
    public abstract class BudgetDurationBaseContract : IApiContract
    {
        public Guid Id { get; set; }
    }
}
