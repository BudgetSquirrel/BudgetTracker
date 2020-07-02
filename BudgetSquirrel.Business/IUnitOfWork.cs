using System.Threading.Tasks;

namespace BudgetSquirrel.Business
{
  public interface IUnitOfWork
  {
    IRepository<T> GetRepository<T>();
    Task SaveChangesAsync();
  }
}