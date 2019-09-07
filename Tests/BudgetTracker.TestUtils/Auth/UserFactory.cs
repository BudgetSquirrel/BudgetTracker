using Bogus;
using BudgetTracker.Business.Auth;
using System;

namespace BudgetTracker.TestUtils.Auth
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
            User user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Username = _faker.Internet.UserName(firstName, lastName),
                Password = _faker.Internet.Password(),
                Email = _faker.Internet.Email(firstName, lastName)
            };
            return user;
        }
    }
}
