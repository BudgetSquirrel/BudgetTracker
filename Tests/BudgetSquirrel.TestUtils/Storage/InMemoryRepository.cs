using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BudgetSquirrel.Business.Infrastructure;
using BudgetSquirrel.TestUtils.Infrastructure;

namespace BudgetSquirrel.TestUtils.Storage
{
  public class InMemoryRepository<TModel> : IRepository<TModel> where TModel : class
  {
    private IEnumerable<TModel> collection;

    public InMemoryRepository(IEnumerable<TModel> collection)
    {
      this.collection = collection;
    }

    public void Add(TModel instance)
    {
      this.collection = this.collection.Append(instance);
    }

    public IIncludableQuerySet<TModel> GetAll()
    {
      return new InMemoryIncludableQuerySet<TModel>(this.collection);
    }

    public void Remove(TModel instance)
    {
      if (typeof(TModel).GetProperties().Any(p => p.Name == "Id"))
      {
        PropertyInfo idProperty = typeof(TModel).GetProperty("Id");
        this.collection = this.collection.Where(x => idProperty.GetValue(x) != idProperty.GetValue(instance));
      }
      else
      {
        this.collection = this.collection.Except(new List<TModel>() { instance });
      }
    }

    public void Update(TModel instance)
    {
      this.Remove(instance);
      this.Add(instance);
    }
  }
}