using BudgetTracker.Business.Budgeting;
using Newtonsoft.Json;
using System;

namespace BudgetTracker.Business.Transactions
{
    public class TransactionMessage
    {
        [JsonProperty("id")]
        public Guid? Id { get; set; }

        [JsonProperty("vendor-name")]
        public string VendorName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("date-of-transaction")]
        public DateTime DateOfTransaction { get; set; }

        [JsonProperty("check-number")]
        public string CheckNumber { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("budget-id")]
        public Guid BudgetId { get; set; }
    }
}
