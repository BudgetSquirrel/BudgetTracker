using BudgetTracker.Business.Api.Contracts.AuthenticationApi;
using BudgetTracker.Business.Ports.Repositories;
using BudgetTracker.Common.Models;
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

        public static async Task<bool> IsAccountRegistrationDuplicate(string username, IUserRepository userRepository)
        {
            User duplicateUser = await userRepository.GetByUsername(username);
            return duplicateUser != null;
        }
    }
}
