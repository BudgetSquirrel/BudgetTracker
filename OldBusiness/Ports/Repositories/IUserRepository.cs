using BudgetTracker.Business.Auth;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetTracker.Business.Ports.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// <p>
        /// Fetches all users from the database that have not been deleted. These
        /// are the active users.
        /// </p>
        /// </summary>
        Task<List<User>> GetActiveUsers();

        /// <summary>
        /// Returns the user that has the given username or null if
        /// it doesn't exist. The password on the user returned in this
        /// will be encrypted.
        /// </summary>
        Task<User> GetByUsername(string username);

        /// <summary>
        /// Returns the user that has the given id or null if
        /// it doesn't exist. The password on the user returned in this
        /// will be encrypted.
        /// </summary>
        Task<User> GetById(Guid userId);

        /// <summary>
        /// <para>
        /// Creates the user.
        /// </para>
        /// </summary>
        Task<bool> Register(User userModel);

        Task Delete(Guid userId);
    }
}