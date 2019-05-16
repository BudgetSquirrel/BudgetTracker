using budgettracker.business.Api.Contracts;
using budgettracker.business.Api.Converters;
using budgettracker.common;
using budgettracker.data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace budgettracker.business.Api
{
    public class AuthenticationApi
    {
        private static UserApiConverter _userApiConverter = new UserApiConverter();

        public static ApiResponse Register(UserRequestApiContract userContract, IServiceProvider serviceProvider)
        {
            UserStore userStore = new UserStore(serviceProvider);
            User userModel = _userApiConverter.ToModel(userContract);
            ApiResponse response;
            
            IEnumerable<string> errors;
            if (userStore.Register(userModel, out errors))
            {
                UserResponseApiContract responseData = _userApiConverter.ToResponseContract(userModel);
                response = new ApiResponse(responseData);
            }
            else
            {
                response = new ApiResponse(String.Join(";", errors.ToArray()));
            }

            return response;
        }
    }
}