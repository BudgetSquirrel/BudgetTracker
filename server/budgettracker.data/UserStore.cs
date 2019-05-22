using budgettracker.common;
using budgettracker.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace budgettracker.data
{
    /// <summary>
    /// Contains logic to perform CRUD operations on users. This acts as a
    /// translation between the basic <see cref="User" /> class and the
    /// data model so that the business layer doesn't have to worry about
    /// the data storage implementation.
    /// </summary>
    public class UserStore
    {
        private UserManager<UserModel> _userManager;

        public UserStore(IServiceProvider serviceProvider)
        {
            _userManager = (UserManager<UserModel>) serviceProvider.GetService(typeof(UserManager<UserModel>));
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
        public bool Register(User userData, out IEnumerable<string> errors)
        {
            UserModel user = new UserModel {
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                UserName = userData.UserName,
                Email = userData.Email
            };
            IdentityResult result = _userManager.CreateAsync(user, userData.Password).Result;
            errors = result.Errors.Select(idError => idError.Code); // Codes may include "DuplicateUserName".
            return result.Succeeded;
        }
    }
}