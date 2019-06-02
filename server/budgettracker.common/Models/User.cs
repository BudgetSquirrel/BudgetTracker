using GateKeeper.Models;

namespace budgettracker.common.Models
{
    /// <summary>
    /// Represents a user or data for a user. This may or may
    /// not actually reflect what is currently in the database.
    /// </summary>
    public class User : IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}