using System;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Data.EntityFramework.Models;

namespace BudgetSquirrel.Data.EntityFramework.Repositories.Interfaces
{
  public interface IBudgetRepository
  {
    Task<BudgetRecord> SaveRootBudget(Budget budget, Guid userId);
  }
}