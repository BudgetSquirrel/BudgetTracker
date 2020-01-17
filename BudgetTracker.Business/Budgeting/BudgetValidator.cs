using System;
using System.Collections.Generic;
using BudgetTracker.Business.BudgetPeriods;
using Newtonsoft.Json;

namespace BudgetTracker.Business.Budgeting
{
    public class BudgetValidator
    {
        public static class ErrorMessages
        {
            public const string INVALID_NAME = "INVALID_NAME";
            public const string INVALID_AMOUNT = "INVALID_AMOUNT";
            public const string INVALID_DURATION = "INVALID_DURATION";
            public const string NO_DURATION_EXPECTED = "NO_DURATION_EXPECTED";
        }

        public void ValidateCreateBudgetRequest(CreateBudgetRequestMessage arguments)
        {
            bool isNameValid = arguments.Name != null;

            bool isAmountValid = (arguments.SetAmount == null && arguments.PercentAmount != null && arguments.PercentAmount <= 1.0) ||
                                 (arguments.SetAmount != null && arguments.PercentAmount == null);

            bool isRootBudget = arguments.ParentBudgetId == null;
            bool isDurationValid = true;
            bool isUnexpectedDuration = false;
            if (isRootBudget)
            {
                isDurationValid = IsCreateBudgetDurationRequestValid(arguments.Duration);

            }
            else
            {
                isUnexpectedDuration = arguments.Duration != null;
            }

            List<string> errorMessages = new List<string>();
            if (!isNameValid)
                errorMessages.Add(ErrorMessages.INVALID_NAME);
            if (!isAmountValid)
                errorMessages.Add(ErrorMessages.INVALID_AMOUNT);
            if (!isDurationValid)
                errorMessages.Add(ErrorMessages.INVALID_DURATION);
            if (isUnexpectedDuration)
                errorMessages.Add(ErrorMessages.NO_DURATION_EXPECTED);
            if (!isNameValid || !isAmountValid || !isDurationValid || isUnexpectedDuration)
                throw new InvalidOperationException(
                    JsonConvert.SerializeObject(errorMessages));
        }

        private bool IsCreateBudgetDurationRequestValid(BudgetDurationBaseMessage message)
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

        public void ValidateUpdateBudgetRequest(UpdateBudgetRequestMessage arguments)
        {
            bool isIdPresent = arguments.Id != null;
            bool isNameValid = arguments.Name != null;

            bool isAmountValid = (arguments.SetAmount == null && arguments.PercentAmount != null && arguments.PercentAmount <= 1.0) ||
                                 (arguments.SetAmount != null && arguments.PercentAmount == null);

            bool isRootBudget = arguments.ParentBudgetId == null;
            bool isDurationValid = true;
            bool isUnexpectedDuration = false;
            if (isRootBudget)
            {
                isDurationValid = IsUpdateBudgetDurationRequestValid(arguments.Duration);
            }
            else
            {
                isUnexpectedDuration = arguments.Duration != null;
            }

            List<string> errorMessages = new List<string>();
            if (!isNameValid)
                errorMessages.Add(ErrorMessages.INVALID_NAME);
            if (!isAmountValid)
                errorMessages.Add(ErrorMessages.INVALID_AMOUNT);
            if (!isDurationValid)
                errorMessages.Add(ErrorMessages.INVALID_DURATION);
            if (isUnexpectedDuration)
                errorMessages.Add(ErrorMessages.NO_DURATION_EXPECTED);
            if (!isNameValid || !isAmountValid || !isDurationValid || isUnexpectedDuration)
                throw new InvalidOperationException(
                    JsonConvert.SerializeObject(errorMessages));
        }

        private bool IsUpdateBudgetDurationRequestValid(BudgetDurationBaseMessage message)
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
