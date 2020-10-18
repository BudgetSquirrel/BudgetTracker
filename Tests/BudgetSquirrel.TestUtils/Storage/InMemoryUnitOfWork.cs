using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetSquirrel.Business;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Business.Infrastructure;

namespace BudgetSquirrel.TestUtils.Storage
{
  public class InMemoryUnitOfWork : IUnitOfWork
  {
    public IRepository<User> UserRepo { get; private set; } = new InMemoryRepository<User>(new User[] {});
    public IRepository<BudgetDurationBase> BudgetDurationRepo { get; private set; } = new InMemoryRepository<BudgetDurationBase>(new BudgetDurationBase[] {});
    public IRepository<Fund> FundRepo { get; private set; } = new InMemoryRepository<Fund>(new Fund[] {});
    public IRepository<Budget> BudgetRepo { get; private set; } = new InMemoryRepository<Budget>(new Budget[] {});
    public IRepository<BudgetPeriod> BudgetPeriodRepo { get; private set; } = new InMemoryRepository<BudgetPeriod>(new BudgetPeriod[] {});

    public IRepository<T> GetRepository<T>() where T : class
    {
      if (typeof(T) == typeof(Budget))
      {
        return (IRepository<T>) this.BudgetRepo;
      }
      else if (typeof(T) == typeof(BudgetDurationBase))
      {
        return (IRepository<T>) this.BudgetDurationRepo;
      }
      else if (typeof(T) == typeof(BudgetPeriod))
      {
        return (IRepository<T>) this.BudgetPeriodRepo;
      }
      else if (typeof(T) == typeof(Fund))
      {
        return (IRepository<T>) this.FundRepo;
      }
      else if (typeof(T) == typeof(User))
      {
        return (IRepository<T>) this.UserRepo;
      }
      else
      {
        throw new InvalidOperationException("Cannot find repository for type " + nameof(T));
      }
    }

    public Task SaveChangesAsync()
    {
      return Task.CompletedTask;
    }
  }
}