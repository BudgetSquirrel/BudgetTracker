using BudgetTracker.BudgetSquirrel.Application;
using BudgetTracker.BudgetSquirrel.Application.Messages;
using BudgetTracker.BudgetSquirrel.Application.Messages.AuthenticationApi;
using BudgetTracker.Business.Converters;
using BudgetTracker.Business.Auth;
using BudgetTracker.Business;
using BudgetTracker.Common;
using BudgetTracker.Business.Ports.Repositories;
using GateKeeper.Configuration;
using GateKeeper.Cryptogrophy;
using GateKeeper.Exceptions;
using GateKeeper.Repositories;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetTracker.BudgetSquirrel.Application
{
    /// <summary>
    /// <p>
    /// Provides an API for the authentication logic for this application. With
    /// this API, one can register new users, login, logout and more.
    /// </p>
    /// </summary>
    public class AuthenticationApi : ApiBase<User>, IAuthenticationApi
    {
        IUserRepository _userRepository;

        public AuthenticationApi(IGateKeeperUserRepository<User> gateKeeperUserRepository, IUserRepository userRepository,
            IConfiguration appConfig)
            : base(gateKeeperUserRepository, new Rfc2898Encryptor(),
                    ConfigurationReader.FromAppConfiguration(appConfig))
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// <p>
        /// Allows a user to register a new account.
        /// </p>
        /// </summary>
        public async Task<ApiResponse> Register(ApiRequest request)
        {
            UserRegistrationArgumentApiMessage arguments = request.Arguments<UserRegistrationArgumentApiMessage>();
            UserRequestApiMessage userValues = arguments.UserValues;
            IUserRepository userRepo = _userRepository as IUserRepository;
            User userModel = UserApiConverter.ToModel(userValues);
            ApiResponse response;

            if (!User.IsAccountRegistrationRequestValid(userValues))
            {
                response = new ApiResponse(Constants.Authentication.ApiResponseErrorCodes.PASSWORD_CONFIRM_INCORRECT);
                return response;
            }
            if (await User.IsAccountRegistrationDuplicate(userValues.UserName, userRepo))
            {
                response = new ApiResponse(Constants.Authentication.ApiResponseErrorCodes.DUPLICATE_USERNAME);
                return response;
            }
            if (await userRepo.Register(userModel))
            {
                userModel = await _userRepository.GetByUsername(userModel.Username);
                UserResponseApiMessage responseData = UserApiConverter.ToResponseMessage(userModel);
                response = new ApiResponse(responseData);
            }
            else
            {
                response = new ApiResponse(Constants.Authentication.ApiResponseErrorCodes.UNKNOWN);
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

            UserResponseApiMessage responseData = UserApiConverter.ToResponseMessage(authenticatedUser);
            response = new ApiResponse(responseData);
            return response;
        }

        public async Task<ApiResponse> DeleteUser(ApiRequest request)
        {
            ApiResponse response;
            User authenticatedUser = await Authenticate(request);

            try
            {
                await ((IUserRepository) _userRepository).Delete(authenticatedUser.Id.Value);
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
