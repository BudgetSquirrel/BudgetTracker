using System;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Data.EntityFramework.Models;
using BudgetSquirrel.Data.EntityFramework.Repositories.Interfaces;

namespace BudgetSquirrel.Data.EntityFramework.Repositories.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BudgetSquirrelContext dbContext;
        public AccountRepository(BudgetSquirrelContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Task<UserRecord> CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserRecord> GetUserById()
        {
            throw new NotImplementedException();
        }

        public Task<UserRecord> GetUserByUsername()
        {
            throw new NotImplementedException();
        }
    }
}