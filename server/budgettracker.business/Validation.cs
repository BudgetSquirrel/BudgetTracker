using System;
using budgettracker.business.Api.Contracts.AuthenticationApi;
using budgettracker.business.Api.Contracts.BudgetApi;

namespace budgettracker.business
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
            Console.WriteLine(arguments.Name + " " + arguments.SetAmount + " " + arguments.Duration);
            return arguments.Name != null &&
                arguments.SetAmount != 0M && 
                arguments.Duration != 0;
                 
        }
    }
}