using budgettracker.business.Api.Contracts;
using budgettracker.business.Api.Contracts.AuthenticationApi;
using budgettracker.business.Api.Contracts.Responses;
using budgettracker.business.Api.Contracts.Requests;
using budgettracker.business.Api.Converters;
using budgettracker.business.Authentication;
using budgettracker.common;
using budgettracker.common.Authentication;
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
        /// Main entry point to the Auth API. This takes the request and routes it to the
        /// appropriate action function, returning the result. All requests dealing with
        /// user accounts and authentication should be directed here.
        /// </p>
        /// <p>
        /// The API actions available are as follows:
        ///
        /// * REG_USER: Registers a new user with the application.
        /// </p>
        /// </summary>
        public static ApiResponse Invoke(ApiRequest request, IServiceProvider serviceProvider)
        {
            ApiResponse response;

            if (request.Action == AuthApiConstants.ACTION_REGISTER_USER)
            {
                UserRegistrationArgumentApiContract registerUserArgs = request.Arguments<UserRegistrationArgumentApiContract>();
                response = Register(registerUserArgs.UserValues, serviceProvider);
            }
            else {
                response = new ApiResponse($"The action {request.Action} is not supported on the Authentication API");
            }

            return response;
        }

        /// <summary>
        /// <p>
        /// Allows a user to register a new account.
        /// </p>
        /// </summary>
        private static ApiResponse Register(UserRequestApiContract userValues, IServiceProvider serviceProvider)
        {
            UserStore userStore = new UserStore(serviceProvider);
            User userModel = _userApiConverter.ToModel(userValues);
            ApiResponse response;
            
            IEnumerable<string> errors = null;
            if (!AccountValidation.IsAccountRegistrationRequestValid(userValues))
            {
                errors = new List<string>() {
                    AuthenticationConstants.ApiResponseErrorCodes.PASSWORD_CONFIRM_INCORRECT
                };
                response = new ApiResponse(String.Join(";", errors.ToArray()));
                return response;
            }
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