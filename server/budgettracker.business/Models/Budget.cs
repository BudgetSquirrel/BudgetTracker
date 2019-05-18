using System;
using System.Collections.Generic;
using System.Text;

namespace budgettracker.business.Models
{
    /// <summary>
    /// Will be the main budget model
    /// </summary>
    public class Budget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SetAmount { get; set; }
        public float PercentAmount { get; set; }

        /// <summary>
        /// The duration this budget exists before it is restarted, calclated in Days.
        /// </summary>
        public int Duration { get; set; }
        public IEnumerable<Budget> SubBudgets { get; set; }

        /// <summary>
        /// Will indicate the day duration started.
        /// </summary>
        public DateTime Start { get; set; }
    }
}
