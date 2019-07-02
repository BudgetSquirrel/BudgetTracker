using BudgetTracker.Common.Exceptions;
using BudgetTracker.Common.Models;
using BudgetTracker.Common.Models.BudgetDurations;
using BudgetTracker.Data;
using BudgetTracker.Data.Models;
using System;
using System.Collections.Generic;

namespace BudgetTracker.Data.Converters
{
    public class BudgetConverter
    {
        public static Budget ToBusinessModel(BudgetModel dataModel)
        {
            Budget budget = new Budget()
            {
                Id = dataModel.Id,
                Name = dataModel.Name,
                SetAmount = dataModel.SetAmount,
                Duration = GetBudgetDuration(dataModel.Duration),
                BudgetStart = dataModel.BudgetStart,
                ParentBudgetId = dataModel.ParentBudgetId,
                Owner = GetOwnerForBudget(dataModel),
            };
            return budget;
        }

        public static List<Budget> ToBusinessModels(List<BudgetModel> dataModels)
        {
            List<Budget> businessConversions = new List<Budget>();
            foreach (BudgetModel data in dataModels)
            {
                Budget budget = BudgetConverter.ToBusinessModel(data);
                businessConversions.Add(budget);
            }
            return businessConversions;
        }

        public static BudgetModel ToDataModel(Budget businessObject)
        {
            return new BudgetModel()
            {
                Id = businessObject.Id,
                Name = businessObject.Name,
                SetAmount = businessObject.SetAmount,
                Duration = GetBudgetDuration(businessObject.Duration),
                BudgetStart = businessObject.BudgetStart,
                ParentBudgetId = businessObject.ParentBudgetId,
                Owner = GetOwnerForBudget(businessObject),
                OwnerId = businessObject.Owner == null ? default(Guid) : businessObject.Owner.Id.Value,
            };
        }

        public static List<BudgetModel> ToDataModels(List<Budget> businessModels)
        {
            List<BudgetModel> dataConversions = new List<BudgetModel>();
            foreach (Budget budgetObect in businessModels)
            {
                BudgetModel data = BudgetConverter.ToDataModel(budgetObect);
                dataConversions.Add(data);
            }
            return dataConversions;
        }

        private static BudgetDurationModel GetBudgetDuration(BudgetDurationBase durationObject)
        {
            BudgetDurationModel dataModel = new BudgetDurationModel();
            dataModel.Id = durationObject.Id;
            if (durationObject is MonthlyBookEndedDuration)
            {
                MonthlyBookEndedDuration bookEndDuration = (MonthlyBookEndedDuration) durationObject;
                dataModel.DurationType = DataConstants.BudgetDuration.TYPE_MONTHLY_BOOKENDS;
                dataModel.StartDayOfMonth = bookEndDuration.StartDayOfMonth;
                dataModel.EndDayOfMonth = bookEndDuration.EndDayOfMonth;
                dataModel.RolloverStartDateOnSmallMonths = bookEndDuration.RolloverStartDateOnSmallMonths;
                dataModel.RolloverEndDateOnSmallMonths = bookEndDuration.RolloverEndDateOnSmallMonths;
            }
            else if (durationObject is MonthlyDaySpanDuration)
            {
                MonthlyDaySpanDuration spanDuration = (MonthlyDaySpanDuration) durationObject;
                dataModel.DurationType = DataConstants.BudgetDuration.TYPE_MONTHLY_SPAN;
                dataModel.NumberDays = spanDuration.NumberDays;
            }
            else
            {
                throw new ConversionException(durationObject.GetType(), typeof(BudgetDurationModel), $"Duration class '{durationObject.GetType().ToString()}' not supported class.");
            }
            return dataModel;
        }

        private static BudgetDurationBase GetBudgetDuration(BudgetDurationModel dataModel)
        {
            BudgetDurationBase durationObject;
            if (dataModel.DurationType == DataConstants.BudgetDuration.TYPE_MONTHLY_BOOKENDS)
            {
                durationObject = new MonthlyBookEndedDuration()
                {
                    Id = dataModel.Id,
                    StartDayOfMonth = dataModel.StartDayOfMonth,
                    EndDayOfMonth = dataModel.EndDayOfMonth,
                    RolloverStartDateOnSmallMonths = dataModel.RolloverStartDateOnSmallMonths,
                    RolloverEndDateOnSmallMonths = dataModel.RolloverEndDateOnSmallMonths
                };
            }
            else if (dataModel.DurationType == DataConstants.BudgetDuration.TYPE_MONTHLY_SPAN)
            {
                durationObject = new MonthlyDaySpanDuration()
                {
                    Id = dataModel.Id,
                    NumberDays = dataModel.NumberDays
                };
            }
            else
            {
                throw new ConversionException(dataModel.GetType(), typeof(BudgetDurationBase), $"DurationType '{dataModel.DurationType}' not supported type.");
            }
            return durationObject;
        }

        private static User GetOwnerForBudget(BudgetModel dataModel)
        {
            User owner = null;
            if (dataModel.Owner == null)
            {
                owner = new User()
                {
                    Id = dataModel.OwnerId
                };
            }
            else
            {
                owner = new UserConverter().ToBusinessModel(dataModel.Owner);
            }
            return owner;
        }

        private static UserModel GetOwnerForBudget(Budget businessObject)
        {
            UserModel owner = null;
            if (businessObject.Owner != null) {
                owner = new UserConverter().ToDataModel(businessObject.Owner);
            }
            return owner;
        }
    }
}
