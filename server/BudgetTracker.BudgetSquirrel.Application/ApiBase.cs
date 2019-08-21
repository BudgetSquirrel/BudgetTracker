using BudgetTracker.BudgetSquirrel.Application.Messages;
using GateKeeper;
using GateKeeper.Configuration;
using GateKeeper.Cryptogrophy;
using GateKeeper.Models;
using GateKeeper.Repositories;
using System;
using System.Threading.Tasks;

namespace BudgetTracker.BudgetSquirrel.Application
{
    public class ApiBase<U> where U : IUser
    {
        protected IGateKeeperUserRepository<U> _gateKeeperUserRepository;
        protected ICryptor _cryptor;

        protected GateKeeperConfig _gateKeeperConfig;

        public ApiBase(IGateKeeperUserRepository<U> gateKeeperUserRepository, ICryptor cryptor,
            GateKeeperConfig gateKeeperConfig)
        {
            _gateKeeperUserRepository = gateKeeperUserRepository;
            _cryptor = cryptor;
            _gateKeeperConfig = gateKeeperConfig;
        }

        /// <summary>
        /// Authenticates the user login, returning that user if authorized. Otherwise,
        /// this will throw a <see cref="AuthenticationException" />.
        /// </summary>
        public async Task<U> Authenticate(ApiRequest request)
        {
            U user = await GateKeeper.Authentication.Authenticate(request.User.UserName, request.User.Password,
                _gateKeeperUserRepository, _cryptor, _gateKeeperConfig);
            return user;
        }
    }
}
