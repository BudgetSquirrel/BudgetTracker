using BudgetTracker.Business.BudgetPeriods;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace BudgetTracker.Business.Budgeting
{
    public class CreateBudgetRequestMessage {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("percent-amount")]
        public double? PercentAmount { get; set; }

        [JsonProperty("set-amount")]
        public decimal? SetAmount { get; set; }

        [JsonProperty("starting-balance")]
        public decimal? StartingBalance { get; set; }

        /// <summary>
        /// <p> The duration of the budget cycle in days or months.</p>
        /// </summary>
        [JsonProperty("duration")]
        public BudgetDurationBaseMessage Duration { get; set; }

        /// <summary>
        /// <p>
        /// Allows the user to set a start time, if null the start time will be today.
        /// </p>
        /// </summary>
        [JsonProperty("budget-start")]
        public DateTime? BudgetStart { get; set; }

        /// <summary>
        /// <p> The guid the of the parent budget, if the budget is the 'root'
        /// this will be null. This string is converted to guid later. </p>
        /// </summary>
        [JsonProperty("parent-budget-id")]
        public Guid? ParentBudgetId { get; set; }
    }
}
