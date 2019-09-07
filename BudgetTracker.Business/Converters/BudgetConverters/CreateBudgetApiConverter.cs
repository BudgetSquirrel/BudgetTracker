using BudgetTracker.Business.Budgeting;
using BudgetTracker.Business.BudgetPeriods;
using BudgetTracker.Common.Exceptions;
using BudgetTracker.Business.Auth;
using BudgetTracker.BudgetSquirrel.Application.Messages.BudgetApi;

using System;
using System.Collections.Generic;

namespace BudgetTracker.Business.Converters.BudgetConverters
{
    public class CreateBudgetApiConverter
    {
        public static Budget ToModel(CreateBudgetRequestMessage requestMessage)
        {
            return new Budget()
            {
                Name = requestMessage.Name,
                PercentAmount = requestMessage.PercentAmount,
                SetAmount = requestMessage.SetAmount,
                Duration = GeneralBudgetApiConverter.GetBudgetDuration(requestMessage.Duration),
                ParentBudgetId = requestMessage.ParentBudgetId,
                BudgetStart = requestMessage.BudgetStart ?? new DateTime()
            };
        }
    }
}
