using BudgetTracker.Business.Api.Messages;
using BudgetTracker.Business.Api.Messages.AuthenticationApi;
using BudgetTracker.Business.Auth;
using BudgetTracker.Common;
using System;

namespace BudgetTracker.Business.Api.Converters
{
    public class UserApiConverter : IApiConverter<User, UserRequestApiMessage, UserResponseApiMessage>
    {
        public User ToModel(UserRequestApiMessage contract)
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

        public User ToModel(UserResponseApiMessage contract)
        {
            User user = new User() {
                Id = contract.Id,
                FirstName = contract.FirstName,
                LastName = contract.LastName,
                Username = contract.UserName,
                Email = contract.Email
            };
            return user;
        }

        public UserRequestApiMessage ToRequestMessage(User model)
        {
            UserRequestApiMessage contract = new UserRequestApiMessage() {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Password = model.Password,
                Email = model.Email
            };
            return contract;
        }

        public UserResponseApiMessage ToResponseMessage(User model)
        {
            UserResponseApiMessage contract = new UserResponseApiMessage() {
                Id = model.Id.Value,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Email = model.Email
            };
            return contract;
        }
    }
}
