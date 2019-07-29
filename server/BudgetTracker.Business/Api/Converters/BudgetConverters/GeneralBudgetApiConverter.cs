using BudgetTracker.Common.Exceptions;
using BudgetTracker.Common.Models;
using BudgetTracker.Common.Models.BudgetDurations;
using BudgetTracker.Business.Api.Contracts.BudgetApi;
using BudgetTracker.Business.Api.Contracts.BudgetApi.CreateBudget;
using BudgetTracker.Business.Api.Contracts.BudgetApi.BudgetDurations;

using System;
using System.Collections.Generic;

namespace BudgetTracker.Business.Api.Converters.BudgetConverters
{
    public class GeneralBudgetApiConverter
    {
        public static BudgetDurationBase GetBudgetDuration(BudgetDurationBaseContract durationContract)
        {
            if (durationContract == null)
            {
                return null;
            }
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

        public static BudgetDurationBaseContract GetBudgetDuration(BudgetDurationBase durationModel)
        {
            if (durationModel == null)
            {
                return null;
            }
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

        public static BudgetResponseContract ToGeneralResponseContract(Budget model)
        {
            BudgetResponseContract responseContract = new BudgetResponseContract()
            {
                Id = model.Id,
                Name = model.Name,
                PercentAmount = model.PercentAmount,
                SetAmount = model.SetAmount.Value,
                Duration = GetBudgetDuration(model.Duration),
                BudgetStart = model.BudgetStart,
                ParentBudgetId = model.ParentBudgetId
            };

            if (model.SubBudgets != null)
            {
                responseContract.SubBudgets = ToGeneralResponseContracts(model.SubBudgets);
            }

            return responseContract;
        }

        public static List<BudgetResponseContract> ToGeneralResponseContracts(List<Budget> budgets)
        {
            List<BudgetResponseContract> responseContracts = new List<BudgetResponseContract>();
            foreach (Budget budget in budgets)
            {
                responseContracts.Add(ToGeneralResponseContract(budget));
            }
            return responseContracts;
        }
    }
}
