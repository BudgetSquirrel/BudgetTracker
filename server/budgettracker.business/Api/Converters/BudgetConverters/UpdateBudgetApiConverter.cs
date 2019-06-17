using budgettracker.common.Models;
using budgettracker.business.Api.Contracts.BudgetApi.UpdateBudget;
using budgettracker.common.Models.BudgetDurations;
using budgettracker.business.Api.Contracts.BudgetApi.BudgetDurations;
using budgettracker.common.Exceptions;

namespace budgettracker.business.Api.Converters.BudgetConverters
{
    public class UpdateBudgetApiConverter
    {        public static Budget ToModel(UpdateBudgetRequestContract requestContract)
        {
            return new Budget()
            {
                Id = requestContract.Id,
                Name = requestContract.Name,
                SetAmount = requestContract.SetAmount,
                Duration = GetBudgetDuration(requestContract.Duration),
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
                SetAmount = model.SetAmount,
                Duration = GetBudgetDuration(model.Duration),
                BudgetStart = model.BudgetStart,
                ParentBudgetId = model.ParentBudgetId,    
            };
        }

        private BudgetDurationBase GetBudgetDuration(BudgetDurationBaseContract durationContract)
        {
            BudgetDurationBase durationModel = null;
            if (durationContract is MonthlyBookEndedDurationContract)
            {
                MonthlyBookEndedDurationContract bookEndDurationContract = (MonthlyBookEndedDurationContract) durationContract;
                durationModel = new MonthlyBookEndedDuration()
                {
                    Id = bookEndDurationContract.Id,
                    StartDayOfMonth = bookEndDurationContract.StartDayOfMonth,
                    EndDayOfMonth = bookEndDurationContract.EndDayOfMonth,
                    RolloverStartDateOnSmallMonths = bookEndDurationContract.RolloverStartDateOnSmallMonths,
                    RolloverEndDateOnSmallMonths = bookEndDurationContract.RolloverEndDateOnSmallMonths,
                };
            }
            else if (durationContract is MonthlyDaySpanDurationContract)
            {
                MonthlyDaySpanDurationContract daySpanDurationContract = (MonthlyDaySpanDurationContract) durationContract;
                durationModel = new MonthlyDaySpanDuration()
                {
                    Id = daySpanDurationContract.Id,
                    NumberDays = daySpanDurationContract.NumberDays
                };
            }
            else
            {
                throw new ConversionException(durationContract.GetType(), typeof(BudgetDurationBase), $"Duration class '{durationContract.GetType().ToString()}' not supported class.");
            }
            return durationModel;
        }

        private BudgetDurationBaseContract GetBudgetDuration(BudgetDurationBase durationModel)
        {
            BudgetDurationBaseContract durationContract = null;
            if (durationModel is MonthlyBookEndedDuration)
            {
                MonthlyBookEndedDuration bookEndDurationModel = (MonthlyBookEndedDuration) durationModel;
                durationContract = new MonthlyBookEndedDurationContract()
                {
                    Id = bookEndDurationModel.Id,
                    StartDayOfMonth = bookEndDurationModel.StartDayOfMonth,
                    EndDayOfMonth = bookEndDurationModel.EndDayOfMonth,
                    RolloverStartDateOnSmallMonths = bookEndDurationModel.RolloverStartDateOnSmallMonths,
                    RolloverEndDateOnSmallMonths = bookEndDurationModel.RolloverEndDateOnSmallMonths,
                };
            }
            else if (durationModel is MonthlyDaySpanDuration)
            {
                MonthlyDaySpanDuration daySpanDurationModel = (MonthlyDaySpanDuration) durationModel;
                durationContract = new MonthlyDaySpanDurationContract()
                {
                    Id = daySpanDurationModel.Id,
                    NumberDays = daySpanDurationModel.NumberDays
                };
            }
            else
            {
                throw new ConversionException(durationModel.GetType(), typeof(BudgetDurationBaseContract), $"Duration class '{durationModel.GetType().ToString()}' not supported class.");
            }
            return durationContract;
        }
    }
}