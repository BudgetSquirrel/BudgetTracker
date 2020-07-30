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

    public virtual void Add(TModel instance) => this.context.Set<TModel>().Add(instance);

    public virtual void Remove(TModel instance) => this.context.Set<TModel>().Remove(instance);

    public virtual void Update(TModel instance) => this.context.Set<TModel>().Update(instance);

    public virtual IQueryable<TModel> GetAll()
    {
      return this.context.Set<TModel>();
    }
  }
}