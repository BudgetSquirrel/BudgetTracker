using System;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.Data.EntityFramework.Repositories.Interfaces
{
  public interface IBudgetRepository
  {
    Task<Budget> SaveRootBudget(Budget budget, Guid userId);

    IQueryable<Budget> GetBudgets();
  }
}