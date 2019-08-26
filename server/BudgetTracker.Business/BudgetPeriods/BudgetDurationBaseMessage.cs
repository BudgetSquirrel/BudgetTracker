using Newtonsoft.Json;
using System;

namespace BudgetTracker.Business.Budgeting.BudgetPeriods
{
    public abstract class BudgetDurationBaseMessage
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
