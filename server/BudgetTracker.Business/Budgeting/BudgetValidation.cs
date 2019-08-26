using BudgetTracker.BudgetSquirrel.Application.Messages.BudgetApi;
using BudgetTracker.Business.Budgeting.BudgetPeriods;

namespace BudgetTracker.Business.Budgeting
{
    public class BudgetValidation
    {
        public static bool IsCreateBudgetRequestValid(CreateBudgetRequestMessage arguments)
        {
            bool isValid = arguments.Name != null;

            bool isAmountValid = (arguments.SetAmount == null && arguments.PercentAmount != null && arguments.PercentAmount <= 1.0) ||
                                 (arguments.SetAmount != null && arguments.PercentAmount == null);

            isValid = isValid && isAmountValid;

            if (!isValid) return false;

            bool isRootBudget = arguments.ParentBudgetId == null;
            if (isRootBudget)
            {
                isValid = isValid && IsCreateBudgetDurationRequestValid(arguments.Duration);
            }
            else
            {
                isValid = isValid && arguments.Duration == null;
            }
            return isValid;
        }

        private static bool IsCreateBudgetDurationRequestValid(BudgetDurationBaseMessage message)
        {
            if (message == null)
            {
                return false;
            }
            if (message is MonthlyBookEndedDurationMessage)
            {
                MonthlyBookEndedDurationMessage casted = (MonthlyBookEndedDurationMessage) message;
                if (casted.StartDayOfMonth < 1 || casted.StartDayOfMonth > 31)
                    return false;
                else if (casted.EndDayOfMonth < 1 || casted.EndDayOfMonth > 31)
                    return false;
                return true;
            }
            else if (message is MonthlyDaySpanDurationMessage)
            {
                MonthlyDaySpanDurationMessage casted = (MonthlyDaySpanDurationMessage) message;
                if (casted.NumberDays < 1)
                    return false;
                return true;
            }
            else return false;
        }

        public static bool IsUpdateBudgetRequestValid(UpdateBudgetRequestMessage arguments)
        {
            bool isValid = arguments.Id != null &&
                arguments.Name != null;

            bool isRootBudget = arguments.ParentBudgetId == null;
            if (isRootBudget)
            {
                isValid = isValid && IsUpdateBudgetDurationRequestValid(arguments.Duration);
            }
            else
            {
                isValid = isValid && arguments.Duration == null;
            }

            bool isAmountValid = (arguments.SetAmount == null && arguments.PercentAmount != null && arguments.PercentAmount <= 1.0) ||
                                 (arguments.SetAmount != null && arguments.PercentAmount == null);

            isValid = isValid && isAmountValid;

            return isValid;
        }

        private static bool IsUpdateBudgetDurationRequestValid(BudgetDurationBaseMessage message)
        {
            if (message == null)
            {
                return false;
            }
            if (message is MonthlyBookEndedDurationMessage)
            {
                MonthlyBookEndedDurationMessage casted = (MonthlyBookEndedDurationMessage) message;
                if (casted.StartDayOfMonth < 1 || casted.StartDayOfMonth > 31)
                    return false;
                else if (casted.EndDayOfMonth < 1 || casted.EndDayOfMonth > 31)
                    return false;
                return true;
            }
            else if (message is MonthlyDaySpanDurationMessage)
            {
                MonthlyDaySpanDurationMessage casted = (MonthlyDaySpanDurationMessage) message;
                if (casted.NumberDays < 1)
                    return false;
                return true;
            }
            else return false;
        }
    }
}
