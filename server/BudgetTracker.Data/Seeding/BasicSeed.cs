using Bogus;
using BudgetTracker.Data;
using BudgetTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetTracker.Data.Seeding
{
    public class BasicSeed
    {
        private BudgetTrackerContext _dbContext;
        private Faker _faker;

        public BasicSeed(BudgetTrackerContext dbContext)
        {
            _dbContext = dbContext;
            _faker = new Faker();
        }

        public async Task Seed()
        {
            Console.WriteLine("Seeding");
            List<UserModel> users = SeedUsers();
            UserModel mainUser = users.First();
            BudgetModel rootBudget = RandomBudget(mainUser);

            BudgetModel b1 = RandomBudget(mainUser, rootBudget);
            BudgetModel b2 = RandomBudget(mainUser, b1);
            BudgetModel b3 = RandomBudget(mainUser, b1);
            BudgetModel b4 = RandomBudget(mainUser, b1);
            BudgetModel b5 = RandomBudget(mainUser, b1);
            BudgetModel b6 = RandomBudget(mainUser, b2);
            BudgetModel b7 = RandomBudget(mainUser, b6);
            BudgetModel b8 = RandomBudget(mainUser, b6);

            await _dbContext.SaveChangesAsync();
        }

        public List<UserModel> SeedUsers()
        {
            List<UserModel> users = new List<UserModel>()
            {
                new UserModel()
                {
                    UserName = "user1",
                    PassWord = "user1234",
                    Email = "user1@gmail.com",
                    FirstName = "User",
                    LastName = "One"
                },
                new UserModel()
                {
                    UserName = "squirrel_man",
                    PassWord = "user1234",
                    Email = "squirrel_man@gmail.com",
                    FirstName = "Squirrel",
                    LastName = "Acorn"
                }
            };

            _dbContext.AddRange(users);
            return users;
        }

        public BudgetModel RandomBudget(UserModel user)
        {
            BudgetModel budget = RandomBudgetValues();
            budget.OwnerId = user.Id;
            budget.Duration = new BudgetDurationModel()
            {
                DurationType = DataConstants.BudgetDuration.TYPE_MONTHLY_SPAN,
                NumberDays = _faker.Random.Number(31)
            };

            _dbContext.Add(budget);
            return budget;
        }

        public BudgetModel RandomBudget(UserModel user, BudgetModel parent)
        {
            BudgetModel budget = RandomBudgetValues();
            budget.ParentBudgetId = parent.Id;
            budget.OwnerId = user.Id;
            budget.Duration = parent.Duration;

            _dbContext.Add(budget);
            return budget;
        }

        private BudgetModel RandomBudgetValues()
        {
            BudgetModel budget = new BudgetModel()
            {
                Name = _faker.Lorem.Word(),
                SetAmount = _faker.Finance.Amount(),
                BudgetStart = DateTime.Now,
                CreatedDate = DateTime.Now,
            };
            return budget;
        }
    }
}
