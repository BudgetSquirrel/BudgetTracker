using BudgetTracker.Business.Ports.Exceptions;
using BudgetTracker.Business.Ports.Repositories;
using BudgetTracker.Business.Auth;
using BudgetTracker.Data.EntityFramework.Converters;
using BudgetTracker.Data.EntityFramework.Models;
using GateKeeper;
using GateKeeper.Configuration;
using GateKeeper.Cryptogrophy;
using GateKeeper.Exceptions;
using GateKeeper.Models;
using GateKeeper.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetTracker.Data.EntityFramework.Repositories
{
    /// <summary>
    /// Contains logic to perform CRUD operations on users. This acts as a
    /// translation between the basic <see cref="User" /> class and the
    /// data model so that the business layer doesn't have to worry about
    /// the data storage implementation.
    /// </summary>
    public class UserRepository : IUserRepository, IGateKeeperUserRepository<User>
    {
        GateKeeperConfig _gateKeeperConfig;

        BudgetTrackerContext _dbContext;

        UserConverter _userConverter;
        ICryptor _cryptor;

        public UserRepository(BudgetTrackerContext dbContext, IConfiguration appConfig)
        {
            _gateKeeperConfig = ConfigurationReader.FromAppConfiguration(appConfig);

            _dbContext = dbContext;

            _userConverter = new UserConverter();
            _cryptor = new Rfc2898Encryptor();
        }

        /// <summary>
        /// <p>
        /// Fetches all users from the database that have not been deleted. These
        /// are the active users.
        /// </p>
        /// </summary>
        public async Task<List<User>> GetActiveUsers()
        {
            List<UserModel> userData = await GetActiveUsersFromDb().ToListAsync();
            List<User> userModels = _userConverter.ToBusinessModels(userData);
            return userModels;
        }

        /// <summary>
        /// <p>
        /// Fetches all users from the database that have not been deleted. These
        /// are the active users.
        /// </p>
        /// </summary>
        private IQueryable<UserModel> GetActiveUsersFromDb()
        {
            IQueryable<UserModel> users = _dbContext.Users.Where(u => !u.DateDeleted.HasValue);
            return users;
        }

        /// <summary>
        /// Returns the user that has the given username or null if
        /// it doesn't exist. The password on the user returned in this
        /// will be encrypted.
        /// </summary>
        public async Task<User> GetByUsername(string username)
        {
            UserModel userData = await GetActiveUsersFromDb().Where(u => u.UserName == username).SingleOrDefaultAsync();
            if (userData == null)
            {
                return null;
            }
            User user = _userConverter.ToBusinessModel(userData);
            return user;
        }

        /// <summary>
        /// <para>
        /// Creates the user.
        /// </para>
        /// </summary>
        public async Task<bool> Register(User userModel)
        {
            string encryptedPassword = _cryptor.Encrypt(userModel.Password, _gateKeeperConfig.EncryptionKey, _gateKeeperConfig.Salt);
            userModel.Password = encryptedPassword;

            UserModel userData = _userConverter.ToDataModel(userModel);
            _dbContext.Users.Add(userData);
            int recordsSaved = await _dbContext.SaveChangesAsync();
            return recordsSaved >= 1;
        }

        public async Task Delete(Guid userId)
        {
            UserModel user = await GetActiveUsersFromDb().Where(u => u.Id == userId).SingleAsync();
            user.DateDeleted = DateTime.Now;
            await _dbContext.SaveChangesAsync();
        }
    }
}
