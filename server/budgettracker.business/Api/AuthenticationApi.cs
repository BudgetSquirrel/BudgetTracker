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
using GateKeeper.Exceptions;
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
            UserRegistrationArgumentApiContract arguments = request.Arguments<UserRegistrationArgumentApiContract>();
            UserRequestApiContract userValues = arguments.UserValues;
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

        /// <summary>
        /// <p>
        /// Authenticates the user, returning it in the response if authorized.
        /// </p>
        /// </summary>
        public static ApiResponse Authenticate(ApiRequest request, IServiceProvider serviceProvider)
        {
            LoginArgumentsApiContract arguments = request.Arguments<LoginArgumentsApiContract>();
            UserRequestApiContract userValues = arguments.Credentials;
            UserStore userStore = new UserStore(serviceProvider);
            ApiResponse response;

            try {
                User authenticatedUser = userStore.Authenticate(userValues.UserName, userValues.Password);
                UserResponseApiContract responseData = _userApiConverter.ToResponseContract(authenticatedUser);
                response = new ApiResponse(responseData);
            }
            catch (AuthenticationException)
            {
                response = new ApiResponse("Those credentials could not be authorized.");
            }
            return response;
        }
    }
}