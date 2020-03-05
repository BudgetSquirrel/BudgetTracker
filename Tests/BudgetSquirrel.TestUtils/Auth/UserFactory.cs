using Bogus;
using BudgetSquirrel.Business.Auth;
using System;

namespace BudgetSquirrel.TestUtils.Auth
{
    public class UserFactory
    {
        private Faker _faker;

        public UserFactory()
        {
            _faker = new Faker();
        }

        public User NewUser()
        {
            string firstName = _faker.Name.FirstName();
            string lastName = _faker.Name.LastName();
            string username = _faker.Internet.UserName(firstName, lastName);
            string email = _faker.Internet.Email(firstName, lastName);
            User user = new User(Guid.NewGuid(), username, firstName, lastName, email);
            return user;
        }
    }
}
