using budgettracker.business.Api.Contracts;
using budgettracker.business.Api.Contracts.AuthenticationApi;
using budgettracker.common;
using budgettracker.common.Models;
using System;

namespace budgettracker.business.Api.Converters
{
    public class UserApiConverter : IApiConverter<User, UserRequestApiContract, UserResponseApiContract>
    {
        public User ToModel(UserRequestApiContract contract)
        {
            User user = new User() {
                FirstName = contract.FirstName,
                LastName = contract.LastName,
                Username = contract.UserName,
                Password = contract.Password,
                Email = contract.Email
            };
            return user;
        }

        public User ToModel(UserResponseApiContract contract)
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

        public UserRequestApiContract ToRequestContract(User model)
        {
            UserRequestApiContract contract = new UserRequestApiContract() {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Password = model.Password,
                Email = model.Email
            };
            return contract;
        }

        public UserResponseApiContract ToResponseContract(User model)
        {
            UserResponseApiContract contract = new UserResponseApiContract() {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Email = model.Email
            };
            return contract;
        }
    }
}
