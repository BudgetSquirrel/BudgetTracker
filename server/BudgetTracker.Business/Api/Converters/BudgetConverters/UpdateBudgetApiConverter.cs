using BudgetTracker.Business.Budgeting;
using BudgetTracker.Business.Budgeting.BudgetPeriods;
using BudgetTracker.Common.Models;
using BudgetTracker.Business.Api.Contracts.BudgetApi.UpdateBudget;
using BudgetTracker.Common.Exceptions;

namespace BudgetTracker.Business.Api.Converters.BudgetConverters
{
    public class UpdateBudgetApiConverter
    {
        public static Budget ToModel(UpdateBudgetRequestContract requestContract)
        {
            return new Budget()
            {
                Id = requestContract.Id,
                Name = requestContract.Name,
                PercentAmount = requestContract.PercentAmount,
                SetAmount = requestContract.SetAmount,
                Duration = GeneralBudgetApiConverter.GetBudgetDuration(requestContract.Duration),
                ParentBudgetId = requestContract.ParentBudgetId,
                BudgetStart = requestContract.BudgetStart
            };
        }
    }
}
