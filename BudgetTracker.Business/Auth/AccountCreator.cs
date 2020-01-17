using System;
using System.Threading.Tasks;
using BudgetTracker.Business.Ports.Repositories;

namespace BudgetTracker.Business.Auth
{
    public class AccountCreator
    {
        public static class AccountCreationErrorCodes
        {
            public const string PASSWORD_CONFIRM_INCORRECT = "PASSWORD_CONFIRM_INCORRECT";
            public const string DUPLICATE_USERNAME = "DUPLICATE_USERNAME";
            public const string UNKNOWN = "UNKNOWN";
        }

        private IUserRepository _userRepository;

        public AccountCreator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Register(RegisterUserMessage arguments)
        {
            IUserRepository userRepo = _userRepository as IUserRepository;
            User userModel = GetUserFromMessage(arguments);

            if ((arguments.Password != arguments.PasswordConfirm))
                throw new InvalidOperationException(AccountCreationErrorCodes.PASSWORD_CONFIRM_INCORRECT);

            User duplicateUser = await _userRepository.GetByUsername(arguments.UserName);
            if (duplicateUser != null)
                throw new InvalidOperationException(AccountCreationErrorCodes.DUPLICATE_USERNAME);

            bool success = await _userRepository.Register(userModel);
            if (!success)
                throw new InvalidOperationException(AccountCreationErrorCodes.UNKNOWN);
        }

        private User GetUserFromMessage(RegisterUserMessage contract)
        {
            User user = new User() {
                Id = contract.Id,
                FirstName = contract.FirstName,
                LastName = contract.LastName,
                Username = contract.UserName,
                Password = contract.Password,
                Email = contract.Email
            };
            return user;
        }
    }
}