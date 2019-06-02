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
        UserApiConverter _userApiConverter = new UserApiConverter();
        UserRepository _userRepository;

        public AuthenticationApi(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// <p>
        /// Allows a user to register a new account.
        /// </p>
        /// </summary>
        public ApiResponse Register(ApiRequest request)
        {
            UserRegistrationArgumentApiContract arguments = request.Arguments<UserRegistrationArgumentApiContract>();
            UserRequestApiContract userValues = arguments.UserValues;
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
            if (_userRepository.Register(userModel, out errors))
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
        public ApiResponse Authenticate(ApiRequest request)
        {
            LoginArgumentsApiContract arguments = request.Arguments<LoginArgumentsApiContract>();
            UserRequestApiContract userValues = arguments.Credentials;
            ApiResponse response;

            try {
                User authenticatedUser = _userRepository.Authenticate(userValues.UserName, userValues.Password);
                UserResponseApiContract responseData = _userApiConverter.ToResponseContract(authenticatedUser);
                response = new ApiResponse(responseData);
            }
            catch (AuthenticationException)
            {
                response = new ApiResponse("Those credentials could not be authorized.");
            }
            return response;
        }

        public ApiResponse DeleteUser(ApiRequest request)
        {
            UserRequestApiContract user = request.User;
            ApiResponse response;
            return null;
        }
    }
}
