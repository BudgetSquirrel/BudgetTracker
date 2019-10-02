using BudgetTracker.Business.Auth;
using BudgetTracker.Common;
using System;

namespace BudgetTracker.Business.Converters
{
    public class UserApiConverter
    {
        public static User ToModel(UserRequestApiMessage contract)
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

        public static UserResponseApiMessage ToResponseMessage(User model)
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
