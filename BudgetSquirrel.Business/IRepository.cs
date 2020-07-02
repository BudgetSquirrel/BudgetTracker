using System.Linq;
using System.Threading.Tasks;

namespace BudgetSquirrel.Business
{
  public interface IRepository<TModel>
  {
    void Add(TModel instance);

    IQueryable<TModel> GetAll();
  }
}