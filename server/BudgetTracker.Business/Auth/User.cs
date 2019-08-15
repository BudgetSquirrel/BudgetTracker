using BudgetTracker.Business.Ports.Repositories;
using GateKeeper.Models;
using System;
using System.Threading.Tasks;

namespace BudgetTracker.Business.Auth
{
    /// <summary>
    /// Represents a user or data for a user. This may or may
    /// not actually reflect what is currently in the database.
    /// </summary>
    public class User : IUser
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? DateDeleted { get; set; }

        /// <summary>
        /// Validates an incoming request to register a new request.
        /// Specifically, this looks to see if the confirm password
        /// is the same as the regular password.
        /// </summary>
        public static bool IsAccountRegistrationRequestValid(UserRequestApiMessage arguments)
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
