using budgettracker.business.Api.Contracts.Requests;
using GateKeeper;
using GateKeeper.Configuration;
using GateKeeper.Cryptogrophy;
using GateKeeper.Models;
using GateKeeper.Repositories;
using System;

namespace budgettracker.business.Api
{
    public class ApiBase<U> where U : IUser
    {
        protected IUserRepository<U> _userRepository;
        protected ICryptor _cryptor;

        protected GateKeeperConfig _gateKeeperConfig;

        public ApiBase(IUserRepository<U> userRepository, ICryptor cryptor,
            GateKeeperConfig gateKeeperConfig)
        {
            _userRepository = userRepository;
            _cryptor = cryptor;
            _gateKeeperConfig = gateKeeperConfig;
        }

        /// <summary>
        /// Authenticates the user login, returning that user if authorized. Otherwise,
        /// this will throw a <see cref="AuthenticationException" />.
        /// </summary>
        public U Authenticate(ApiRequest request)
        {
            U user = GateKeeper.Authentication.Authenticate(request.User.UserName, request.User.Password,
                _userRepository, _cryptor, _gateKeeperConfig);
            return user;
        }
    }
}
