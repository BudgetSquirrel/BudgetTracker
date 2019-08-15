using BudgetTracker.Business.Budgeting;
using BudgetTracker.Business.Budgeting.BudgetPeriods;
using BudgetTracker.Business.Auth;
using BudgetTracker.Business.Api.Messages.BudgetApi;
using BudgetTracker.Common.Exceptions;

namespace BudgetTracker.Business.Api.Converters.BudgetConverters
{
    public class UpdateBudgetApiConverter
    {
        public static Budget ToModel(UpdateBudgetRequestMessage requestMessage)
        {
            return new Budget()
            {
                Id = requestMessage.Id,
                Name = requestMessage.Name,
                PercentAmount = requestMessage.PercentAmount,
                SetAmount = requestMessage.SetAmount,
                Duration = GeneralBudgetApiConverter.GetBudgetDuration(requestMessage.Duration),
                ParentBudgetId = requestMessage.ParentBudgetId,
                BudgetStart = requestMessage.BudgetStart
            };
        }
    }
}
