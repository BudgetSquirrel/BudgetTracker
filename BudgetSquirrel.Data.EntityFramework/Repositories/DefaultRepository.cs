using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Business;

namespace BudgetSquirrel.Data.EntityFramework.Repositories
{
  public class DefaultRepository<TModel> : IRepository<TModel> where TModel : class
  {
    private BudgetSquirrelContext context;

    public DefaultRepository(BudgetSquirrelContext context)
    {
      this.context = context;
    }

    public void Add(TModel instance) => this.context.Add(instance);

    public void Remove(TModel instance) => this.context.Remove(instance);

    public IQueryable<TModel> GetAll()
    {
      return this.context.Set<TModel>();
    }
  }
}