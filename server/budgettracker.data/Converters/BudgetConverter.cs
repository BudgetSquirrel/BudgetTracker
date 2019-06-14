using budgettracker.common.Exceptions;
using budgettracker.common.Models;
using budgettracker.common.Models.BudgetDurations;
using budgettracker.data;
using budgettracker.data.Models;

namespace budgettracker.data.Converters
{
    public class BudgetConverter : IConverter<Budget, BudgetModel>
    {
        public Budget ToBusinessModel(BudgetModel dataModel)
        {
            return new Budget()
            {
                Id = dataModel.Id,
                Name = dataModel.Name,
                SetAmount = dataModel.SetAmount,
                Duration = GetBudgetDuration(dataModel.Duration),
                BudgetStart = dataModel.BudgetStart,
                ParentBudgetId = dataModel.ParentBudgetId
            };
        }

        public BudgetModel ToDataModel(Budget businessObject)
        {
            return new BudgetModel()
            {
                Id = businessObject.Id,
                Name = businessObject.Name,
                SetAmount = businessObject.SetAmount,
                Duration = GetBudgetDuration(businessObject.Duration),
                BudgetStart = businessObject.BudgetStart,
                ParentBudgetId = businessObject.ParentBudgetId
            };
        }

        private BudgetDurationModel GetBudgetDuration(BudgetDurationBase durationObject)
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
                dataModel.NumDays = spanDuration.NumDays;
            }
            else
            {
                throw new ConversionException(durationObject.GetType(), typeof(BudgetDurationModel), $"Duration class '{durationObject.GetType().ToString()}' not supported class.");
            }
            return dataModel;
        }

        private BudgetDurationBase GetBudgetDuration(BudgetDurationModel dataModel)
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
                    NumDays = dataModel.NumDays
                };
            }
            else
            {
                throw new ConversionException(dataModel.GetType(), typeof(BudgetDurationBase), $"DurationType '{dataModel.DurationType}' not supported type.");
            }
            return durationObject;
        }
    }
}
