using System;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Data.EntityFramework.Models;

namespace BudgetSquirrel.Data.EntityFramework.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<UserRecord> GetUserById();
        Task<UserRecord> GetUserByUsername();
        Task<UserRecord> CreateUser(User user);
        Task DeleteUser(Guid id);
    }
}