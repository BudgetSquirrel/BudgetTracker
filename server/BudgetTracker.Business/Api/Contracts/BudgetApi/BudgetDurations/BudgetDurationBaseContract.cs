using BudgetTracker.Business.Api.Contracts;
using System;

namespace BudgetTracker.Business.Api.Contracts.BudgetApi.BudgetDurations
{
    public abstract class BudgetDurationBaseContract : IApiContract
    {
        public Guid Id { get; set; }
    }
}
