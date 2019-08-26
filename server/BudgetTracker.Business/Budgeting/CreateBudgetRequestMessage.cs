using BudgetTracker.Business.Budgeting.BudgetPeriods;
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
                else if (DurationTemp.ContainsKey("start-day-of-month") &&
                    DurationTemp.ContainsKey("end-day-of-month") &&
                    DurationTemp.ContainsKey("number-days"))
                {
                    throw new JsonSerializationException("Budget duration must be either a bookended duration or a day span duration. It cannot be both.");
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
