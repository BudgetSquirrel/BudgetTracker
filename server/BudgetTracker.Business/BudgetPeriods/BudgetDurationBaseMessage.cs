using BudgetTracker.Business.Api.Contracts;
using Newtonsoft.Json;
using System;

namespace BudgetTracker.Business.Budgeting.BudgetPeriods
{
    public abstract class BudgetDurationBaseMessage : IApiContract
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
