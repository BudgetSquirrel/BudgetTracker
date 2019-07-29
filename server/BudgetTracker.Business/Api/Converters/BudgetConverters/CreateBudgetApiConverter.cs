using BudgetTracker.Business.Budgeting;
using BudgetTracker.Business.Budgeting.Tracking.Periods;
using BudgetTracker.Common.Exceptions;
using BudgetTracker.Common.Models;
using BudgetTracker.Business.Api.Contracts.BudgetApi.CreateBudget;

using System;
using System.Collections.Generic;

namespace BudgetTracker.Business.Api.Converters.BudgetConverters
{
    public class CreateBudgetApiConverter
    {
        public static Budget ToModel(CreateBudgetRequestMessage requestContract)
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
    }
}
