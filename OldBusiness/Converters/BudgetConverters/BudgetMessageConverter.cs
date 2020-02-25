using BudgetTracker.Business.Budgeting;
using BudgetTracker.Business.BudgetPeriods;
using BudgetTracker.Common.Exceptions;
using BudgetTracker.Business.Auth;

using System;
using System.Collections.Generic;

namespace BudgetTracker.Business.Converters.BudgetConverters
{
    public class BudgetMessageConverter
    {
        public Budget ToModel(CreateBudgetRequestMessage requestMessage)
        {
            return new Budget()
            {
                Name = requestMessage.Name,
                PercentAmount = requestMessage.PercentAmount,
                SetAmount = requestMessage.SetAmount,
                Duration = GetBudgetDuration(requestMessage.Duration),
                ParentBudgetId = requestMessage.ParentBudgetId,
                BudgetStart = requestMessage.BudgetStart ?? new DateTime()
            };
        }

        public BudgetResponseMessage ToGeneralResponseMessage(Budget model)
        {
            BudgetResponseMessage responseMessage = new BudgetResponseMessage()
            {
                Id = model.Id,
                Name = model.Name,
                PercentAmount = model.PercentAmount,
                SetAmount = model.SetAmount.Value,
                FundBalance = model.FundBalance,
                Duration = GetBudgetDuration(model.Duration),
                BudgetStart = model.BudgetStart,
                ParentBudgetId = model.ParentBudgetId
            };

            if (model.SubBudgets != null)
            {
                responseMessage.SubBudgets = ToGeneralResponseMessages(model.SubBudgets);
            }

            return responseMessage;
        }

        public List<BudgetResponseMessage> ToGeneralResponseMessages(List<Budget> budgets)
        {
            List<BudgetResponseMessage> responseMessages = new List<BudgetResponseMessage>();
            foreach (Budget budget in budgets)
            {
                responseMessages.Add(ToGeneralResponseMessage(budget));
            }
            return responseMessages;
        }

        public BudgetDurationBase GetBudgetDuration(BudgetDurationBaseMessage durationMessage)
        {
            if (durationMessage == null)
            {
                return null;
            }
            BudgetDurationBase durationModel = null;
            if (durationMessage is MonthlyBookEndedDurationMessage)
            {
                MonthlyBookEndedDurationMessage bookEndDurationMessage = (MonthlyBookEndedDurationMessage) durationMessage;
                durationModel = new MonthlyBookEndedDuration()
                {
                    Id = bookEndDurationMessage.Id,
                    StartDayOfMonth = bookEndDurationMessage.StartDayOfMonth,
                    EndDayOfMonth = bookEndDurationMessage.EndDayOfMonth,
                    RolloverStartDateOnSmallMonths = bookEndDurationMessage.RolloverStartDateOnSmallMonths,
                    RolloverEndDateOnSmallMonths = bookEndDurationMessage.RolloverEndDateOnSmallMonths,
                };
            }
            else if (durationMessage is MonthlyDaySpanDurationMessage)
            {
                MonthlyDaySpanDurationMessage daySpanDurationMessage = (MonthlyDaySpanDurationMessage) durationMessage;
                durationModel = new MonthlyDaySpanDuration()
                {
                    Id = daySpanDurationMessage.Id,
                    NumberDays = daySpanDurationMessage.NumberDays
                };
            }
            else
            {
                throw new ConversionException(durationMessage.GetType(), typeof(BudgetDurationBase), $"Duration class '{durationMessage.GetType().ToString()}' not supported class.");
            }
            return durationModel;
        }

        public BudgetDurationBaseMessage GetBudgetDuration(BudgetDurationBase durationModel)
        {
            if (durationModel == null)
            {
                return null;
            }
            BudgetDurationBaseMessage durationMessage = null;
            if (durationModel is MonthlyBookEndedDuration)
            {
                MonthlyBookEndedDuration bookEndDurationModel = (MonthlyBookEndedDuration) durationModel;
                durationMessage = new MonthlyBookEndedDurationMessage()
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
                durationMessage = new MonthlyDaySpanDurationMessage()
                {
                    Id = daySpanDurationModel.Id,
                    NumberDays = daySpanDurationModel.NumberDays
                };
            }
            else
            {
                throw new ConversionException(durationModel.GetType(), typeof(BudgetDurationBaseMessage), $"Duration class '{durationModel.GetType().ToString()}' not supported class.");
            }
            return durationMessage;
        }
    }
}