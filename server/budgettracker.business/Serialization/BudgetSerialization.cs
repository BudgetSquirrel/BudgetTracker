using System;
using System.Collections.Generic;
using budgettracker.common;
using budgettracker.common.Models;
using budgettracker.business.Serialization.Converters;
using Newtonsoft.Json;

namespace budgettracker.business.Serialization
{
    /// <summary>
    /// Contains logic for serializing <see cref="Budget" /> objects.
    /// </summary>
    public class BudgetSerialization
    {
        #region Private Functions

        #endregion


        #region Public Interface
        /// <summary>
        /// Converts the given <see cref="Budget" /> to a json string.
        /// </summary>
        public static string BudgetToJson(Budget budget)
        {
            BudgetSerializationContract serializable = BudgetSerializationConverter.ModelToSerializationContract(budget);
            String json = JsonConvert.SerializeObject(serializable, Formatting.Indented);
            return json;
        }
        #endregion
    }
}
