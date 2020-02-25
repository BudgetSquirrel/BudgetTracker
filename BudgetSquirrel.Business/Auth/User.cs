using System;

namespace BudgetSquirrel.Business.Auth
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        public User(string username, string firstname, string lastname, string email)
        {
            Username = username;
            FirstName = firstname;
            LastName = lastname;
            Email = email;
        }

        public User(Guid id, string username, string firstname, string lastname, string email)
        {
            Username = username;
            FirstName = firstname;
            LastName = lastname;
            Email = email;
        }
    }
}