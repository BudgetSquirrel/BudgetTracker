using BudgetTracker.Common.Exceptions;
using BudgetTracker.Common.Models;
using BudgetTracker.Business.Api.Contracts.BudgetApi.CreateBudget;
using BudgetTracker.Common.Models.BudgetDurations;
using BudgetTracker.Business.Api.Contracts.BudgetApi.BudgetDurations;

using System;
using System.Collections.Generic;

namespace BudgetTracker.Business.Api.Converters.BudgetConverters
{
    public class CreateBudgetApiConverter
    {
        public static Budget ToModel(CreateBudgetRequestContract requestContract)
        {
            return new Budget()
            {
                Name = requestContract.Name,
                PercentAmount = requestContract.PercentAmount,
                SetAmount = requestContract.SetAmount,
                Duration = GeneralBudgetApiConverter.GetBudgetDuration(requestContract.Duration),
                ParentBudgetId = requestContract.ParentBudgetId,
                BudgetStart = requestContract.BudgetStart ?? new DateTime()
            };
        }

        public static CreateBudgetRequestContract ToRequestContract(Budget model)
        {
            throw new System.NotImplementedException();
        }
    }
}
