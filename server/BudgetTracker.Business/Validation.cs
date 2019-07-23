using BudgetTracker.Business.Api.Contracts.AuthenticationApi;
using BudgetTracker.Business.Api.Contracts.BudgetApi.CreateBudget;
using BudgetTracker.Business.Api.Contracts.BudgetApi.UpdateBudget;
using BudgetTracker.Business.Api.Contracts.BudgetApi.BudgetDurations;
using BudgetTracker.Common.Models;
using BudgetTracker.Data.Repositories;
using System;
using System.Threading.Tasks;

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

        public static async Task<bool> IsAccountRegistrationDuplicate(string username, UserRepository userRepository)
        {
            User duplicateUser = await userRepository.GetByUsername(username);
            return duplicateUser != null;
        }

        public static bool IsCreateBudgetRequestValid(CreateBudgetRequestContract arguments)
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
