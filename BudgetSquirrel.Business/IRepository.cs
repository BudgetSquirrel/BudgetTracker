using System.Linq;
using System.Threading.Tasks;

namespace BudgetSquirrel.Business
{
  public interface IRepository<TModel>
  {
    void Add(TModel instance);

    void Remove(TModel instance);

    void Update(TModel instance);

    IQueryable<TModel> GetAll();
  }
}