using System.Threading.Tasks;
using BudgetSquirrel.Business.Infrastructure;

namespace BudgetSquirrel.Business
{
  public interface IUnitOfWork
  {
    IRepository<T> GetRepository<T>() where T : class;
    Task SaveChangesAsync();
  }
}