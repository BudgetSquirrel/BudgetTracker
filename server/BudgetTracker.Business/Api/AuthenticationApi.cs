using BudgetTracker.Business.Api.Contracts;
using BudgetTracker.Business.Api.Contracts.AuthenticationApi;
using BudgetTracker.Business.Api.Contracts.Responses;
using BudgetTracker.Business.Api.Contracts.Requests;
using BudgetTracker.Business.Api.Converters;
using BudgetTracker.Business;
using BudgetTracker.Common;
using BudgetTracker.Common.Models;
using BudgetTracker.Data;
using BudgetTracker.Data.Repositories;
using GateKeeper.Configuration;
using GateKeeper.Cryptogrophy;
using GateKeeper.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetTracker.Business.Api
{
    /// <summary>
    /// <p>
    /// Provides an API for the authentication logic for this application. With
    /// this API, one can register new users, login, logout and more.
    /// </p>
    /// </summary>
    public class AuthenticationApi : ApiBase<User>
    {
        UserApiConverter _userApiConverter;

        public AuthenticationApi(UserRepository userRepository,
            IConfiguration appConfig)
            : base(userRepository, new Rfc2898Encryptor(),
                    ConfigurationReader.FromAppConfiguration(appConfig))
        {
            _userApiConverter = new UserApiConverter();
        }

        /// <summary>
        /// <p>
        /// Allows a user to register a new account.
        /// </p>
        /// </summary>
        public async Task<ApiResponse> Register(ApiRequest request)
        {
            UserRegistrationArgumentApiContract arguments = request.Arguments<UserRegistrationArgumentApiContract>();
            UserRequestApiContract userValues = arguments.UserValues;
            User userModel = _userApiConverter.ToModel(userValues);
            ApiResponse response;

            IEnumerable<string> errors = null;
            if (!Validation.IsAccountRegistrationRequestValid(userValues))
            {
                errors = new List<string>() {
                    Constants.Authentication.ApiResponseErrorCodes.PASSWORD_CONFIRM_INCORRECT
                };
                response = new ApiResponse(String.Join(";", errors.ToArray()));
                return response;
            }
            if (((UserRepository) _userRepository).Register(userModel, out errors))
            {
                userModel = await _userRepository.GetByUsername(userModel.Username);
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
        public async Task<ApiResponse> AuthenticateUser(ApiRequest request)
        {
            ApiResponse response;
            User authenticatedUser = await Authenticate(request);

            UserResponseApiContract responseData = _userApiConverter.ToResponseContract(authenticatedUser);
            response = new ApiResponse(responseData);
            return response;
        }

        public async Task<ApiResponse> DeleteUser(ApiRequest request)
        {
            ApiResponse response;
            User authenticatedUser = await Authenticate(request);

            try
            {
                await ((UserRepository) _userRepository).Delete(authenticatedUser.Id.Value);
                response = new ApiResponse();
            }
            catch (InvalidOperationException)
            {
                response = new ApiResponse("Could not find the specified user.");
            }
            return response;
        }
    }
}
