using BudgetTracker.Common.Models;
using BudgetTracker.Business.Api.Contracts.BudgetApi.UpdateBudget;
using BudgetTracker.Common.Models.BudgetDurations;
using BudgetTracker.Business.Api.Contracts.BudgetApi.BudgetDurations;
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

        public static Budget ToModel(UpdateBudgetResponseContract responseContract)
        {
            throw new System.NotImplementedException();
        }

        public static UpdateBudgetRequestContract ToRequestContract(Budget model)
        {
            throw new System.NotImplementedException();
        }

        public static UpdateBudgetResponseContract ToResponseContract(Budget model)
        {
            return new UpdateBudgetResponseContract()
            {
                Id = model.Id,
                Name = model.Name,
                PercentAmount = model.PercentAmount,
                SetAmount = model.SetAmount,
                Duration = GeneralBudgetApiConverter.GetBudgetDuration(model.Duration),
                BudgetStart = model.BudgetStart,
                ParentBudgetId = model.ParentBudgetId,
            };
        }
    }
}
