using System;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Data.EntityFramework.Converters;
using BudgetSquirrel.Data.EntityFramework.Models;
using BudgetSquirrel.Data.EntityFramework.Repositories.Interfaces;

namespace BudgetSquirrel.Data.EntityFramework.Repositories.Implementations
{
  public class BudgetRepository : IBudgetRepository
  {
    private readonly BudgetSquirrelContext dbContext;

    public BudgetRepository(BudgetSquirrelContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IQueryable<Budget> GetBudgets()
    {
      return this.dbContext.Budgets;
    }

    public async Task<Budget> SaveRootBudget(Budget record, Guid userId)
    {
      record.UserId = userId;
      this.dbContext.Add(record);
      await this.dbContext.SaveChangesAsync();
      return record;
    }
  }
}