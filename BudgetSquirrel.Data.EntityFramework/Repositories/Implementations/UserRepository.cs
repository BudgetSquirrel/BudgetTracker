using System;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Data.EntityFramework.Converters;
using BudgetSquirrel.Data.EntityFramework.Models;
using BudgetSquirrel.Data.EntityFramework.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetSquirrel.Data.EntityFramework.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly BudgetSquirrelContext dbContext;
        public UserRepository(BudgetSquirrelContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<UserRecord> SaveUser(User user, string password)
        {
            UserRecord record = UserConverter.ToDataModel(user);
            record.Password = password;

            this.dbContext.Users.Add(record);
            await this.dbContext.SaveChangesAsync();
            return record;
        }

        public Task DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserRecord> GetUserById()
        {
            throw new NotImplementedException();
        }

        public Task<UserRecord> GetByUsername(string username)
        {
            return this.dbContext.Users.SingleOrDefaultAsync(u => u.Username == username);
        }
    }
}