using BudgetTracker.Business.Api.Messages;
using Newtonsoft.Json;
using System;

namespace BudgetTracker.Business.Budgeting.BudgetPeriods
{
    public abstract class BudgetDurationBaseMessage : IApiMessage
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
