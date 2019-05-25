using budgettracker.business.Api.Contracts;
using budgettracker.business.Api.Contracts.AuthenticationApi;
using budgettracker.business.Api.Contracts.Responses;
using budgettracker.business.Api.Contracts.Requests;
using budgettracker.business.Api.Converters;
using budgettracker.common;
using budgettracker.common.Models;
using budgettracker.data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace budgettracker.business.Api
{
    /// <summary>
    /// <p>
    /// Provides an API for the authentication logic for this application. With
    /// this API, one can register new users, login, logout and more.
    /// </p>
    /// </summary>
    public class AuthenticationApi
    {
        private static UserApiConverter _userApiConverter = new UserApiConverter();

        /// <summary>
        /// <p>
        /// Allows a user to register a new account.
        /// </p>
        /// </summary>
        public static ApiResponse Register(ApiRequest request, IServiceProvider serviceProvider)
        {
            UserStore userStore = new UserStore(serviceProvider);
            UserRegistrationArgumentApiContract registerUserArgs = request.Arguments<UserRegistrationArgumentApiContract>();
            
            User userModel = _userApiConverter.ToModel(registerUserArgs.UserValues);
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