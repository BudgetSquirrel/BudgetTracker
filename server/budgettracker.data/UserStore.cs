using budgettracker.common;
using budgettracker.common.Authentication;
using budgettracker.common.Models;
using budgettracker.data.Converters;
using budgettracker.data.Models;
using GateKeeper;
using GateKeeper.Configuration;
using GateKeeper.Cryptogrophy;
using GateKeeper.Exceptions;
using GateKeeper.Models;
using GateKeeper.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace budgettracker.data
{
    /// <summary>
    /// Contains logic to perform CRUD operations on users. This acts as a
    /// translation between the basic <see cref="User" /> class and the
    /// data model so that the business layer doesn't have to worry about
    /// the data storage implementation.
    /// </summary>
    public class UserStore : IUserRepository<User>
    {
        IConfiguration _appConfig;
        IServiceProvider _serviceProvider;
        GateKeeperConfig _gateKeeperConfig;

        BudgetTrackerContext _dbContext;

        UserConverter _userConverter;
        ICryptor _cryptor;

        public UserStore(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _appConfig = (IConfiguration) (_serviceProvider.GetService(typeof(IConfiguration)));
            _gateKeeperConfig = ConfigurationReader.FromAppConfiguration(_appConfig);

            _dbContext = (BudgetTrackerContext) (_serviceProvider.GetRequiredService(typeof(BudgetTrackerContext)));

            _userConverter = new UserConverter();
            _cryptor = new Rfc2898Encryptor();
        }

        /// <summary>
        /// Returns the user that has the given username or null if
        /// it doesn't exist. The password on the user returned in this
        /// will be encrypted.
        /// </summary>
        public User GetByUsername(string username)
        {
            UserModel userData = _dbContext.Users.Where(u => u.UserName == username).SingleOrDefault();
            if (userData == null)
            {
                return null;
            }
            User user = _userConverter.ToBusinessModel(userData);
            return user;
        }

        /// <summary>
        /// <para>
        /// Creates the user and returns whether or not it was created.
        /// </para>
        /// <para>
        /// If an error occurs, the errors list will be populated with the
        /// error codes for the errors which occurred.
        /// </para>
        /// </summary>
        public bool Register(User userModel, out IEnumerable<string> errors)
        {
            string encryptedPassword = _cryptor.Encrypt(userModel.Password, _gateKeeperConfig.EncryptionKey, _gateKeeperConfig.Salt);
            userModel.Password = encryptedPassword;
            int numDuplicates = _dbContext.Users.Count(user => user.UserName == userModel.Username);

            if (numDuplicates > 0)
            {
                errors = new List<string>() { AuthenticationConstants.ApiResponseErrorCodes.DUPLICATE_USERNAME };
                return false;
            }

            UserModel userData = _userConverter.ToDataModel(userModel);
            _dbContext.Users.Add(userData);
            int recordsSaved = _dbContext.SaveChanges();

            if (recordsSaved < 1)
            {
                errors = new List<string>() { AuthenticationConstants.ApiResponseErrorCodes.UNKNOWN };
                return false;
            }

            errors = null;
            return true;
        }

        /// <summary>
        /// Authenticates the user login, returning that user if authorized. Otherwise,
        /// this will throw a <see cref="AuthenticationException" />.
        /// </summary>
        public User Authenticate(string username, string passwordGuess)
        {
            User authenticatedUser = Authentication.Authenticate(username, passwordGuess,
                                    this, _cryptor, _gateKeeperConfig);
            return authenticatedUser;
        }
    }
}