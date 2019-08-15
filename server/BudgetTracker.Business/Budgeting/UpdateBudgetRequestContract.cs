using BudgetTracker.Business.Api.Messages;
using BudgetTracker.Business.Budgeting.BudgetPeriods;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BudgetTracker.Business.Budgeting
{
    public class UpdateBudgetRequestMessage : IApiMessage {

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("percent-amount")]
        public double? PercentAmount { get; set; }

        [JsonProperty("set-amount")]
        public decimal? SetAmount { get; set; }

        /// <summary>
        /// <p> The duration of the budget cycle in days or months.</p>
        /// </summary>
        [JsonProperty("duration")]
        public JObject DurationTemp { get; set; }

        public BudgetDurationBaseMessage Duration {
            get {
                if (DurationTemp == null)
                {
                    return null;
                }
                string durationSerialized = JsonConvert.SerializeObject(DurationTemp);
                if (DurationTemp.ContainsKey("start-day-of-month") &&
                    DurationTemp.ContainsKey("end-day-of-month"))
                {
                    return JsonConvert.DeserializeObject<MonthlyBookEndedDurationMessage>(durationSerialized);
                }
                else if (DurationTemp.ContainsKey("number-days"))
                {
                    return JsonConvert.DeserializeObject<MonthlyDaySpanDurationMessage>(durationSerialized);
                }
                else
                {
                    throw new JsonSerializationException("Could not understand the duration request.");
                }
            }
        }

        [JsonProperty("budget-start")]
        public DateTime BudgetStart { get; set; }

        /// <summary>
        /// <p> The guid the of the parent budget, if the budget is the 'root'
        /// this will be null. This string is converted to guid later. </p>
        /// </summary>
        [JsonProperty("parent-budget-id")]
        public Guid? ParentBudgetId { get; set; }
    }
}
