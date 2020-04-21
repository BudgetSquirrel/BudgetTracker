using System;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Data.EntityFramework.Models;

namespace BudgetSquirrel.Data.EntityFramework.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserRecord> GetUserById();
        Task<UserRecord> GetByUsername(string username);
        Task<UserRecord> SaveUser(User user, string password);
        Task DeleteUser(Guid id);
    }
}