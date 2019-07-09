using BudgetTracker.Business.Api.Contracts.AuthenticationApi;
using BudgetTracker.Business.Api.Contracts.BudgetApi.CreateBudget;
using BudgetTracker.Business.Api.Contracts.BudgetApi.UpdateBudget;
using BudgetTracker.Business.Api.Contracts.BudgetApi.BudgetDurations;
using System;

namespace BudgetTracker.Business
{
    /// <summary>
    /// Contains logic to validate data and requests for accounts and
    /// users.
    /// </summary>
    public class Validation
    {
        /// <summary>
        /// Validates an incoming request to register a new request.
        /// Specifically, this looks to see if the confirm password
        /// is the same as the regular password.
        /// </summary>
        public static bool IsAccountRegistrationRequestValid(UserRequestApiContract arguments)
        {
            bool isConfirmPasswordCorrect = (arguments.Password == arguments.PasswordConfirm);
            return isConfirmPasswordCorrect;
        }

        public static bool IsCreateBudgetRequestValid(CreateBudgetRequestContract arguments)
        {
            return arguments.Name != null &&
                arguments.SetAmount != 0M &&
                IsCreateBudgetDurationRequestValid(arguments.Duration);
        }

        private static bool IsCreateBudgetDurationRequestValid(BudgetDurationBaseContract contract)
        {
            if (contract == null)
            {
                return false;
            }
            if (contract is MonthlyBookEndedDurationContract)
            {
                MonthlyBookEndedDurationContract casted = (MonthlyBookEndedDurationContract) contract;
                if (casted.StartDayOfMonth < 1 || casted.StartDayOfMonth > 31)
                    return false;
                else if (casted.EndDayOfMonth < 1 || casted.EndDayOfMonth > 31)
                    return false;
                return true;
            }
            else if (contract is MonthlyDaySpanDurationContract)
            {
                MonthlyDaySpanDurationContract casted = (MonthlyDaySpanDurationContract) contract;
                if (casted.NumberDays < 1)
                    return false;
                return true;
            }
            else return false;
        }

        public static bool IsUpdateBudgetRequestValid(UpdateBudgetRequestContract arguments)
        {
            return arguments.Id != null &&
                arguments.Name != null &&
                arguments.SetAmount != 0M &&
                arguments.Duration != null
                && IsUpdateBudgetDurationRequestValid(arguments.Duration);
        }

        private static bool IsUpdateBudgetDurationRequestValid(BudgetDurationBaseContract contract)
        {
            if (contract == null)
            {
                return false;
            }
            if (contract is MonthlyBookEndedDurationContract)
            {
                MonthlyBookEndedDurationContract casted = (MonthlyBookEndedDurationContract) contract;
                if (casted.StartDayOfMonth < 1 || casted.StartDayOfMonth > 31)
                    return false;
                else if (casted.EndDayOfMonth < 1 || casted.EndDayOfMonth > 31)
                    return false;
                return true;
            }
            else if (contract is MonthlyDaySpanDurationContract)
            {
                MonthlyDaySpanDurationContract casted = (MonthlyDaySpanDurationContract) contract;
                if (casted.NumberDays < 1)
                    return false;
                return true;
            }
            else return false;
        }
    }
}
