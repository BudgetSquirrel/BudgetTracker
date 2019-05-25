using budgettracker.common;
using budgettracker.common.Models;
using budgettracker.data.Converters;
using budgettracker.data.Models;
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
    public class UserStore
    {
        public UserStore(IServiceProvider serviceProvider)
        {
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
            errors = new List<string>();
            return false;
        }
    }
}