using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Infrastructure;

namespace BudgetSquirrel.TestUtils.Infrastructure
{
  public class InMemoryThenIncludableQuerySet<TModel, TPreviousProperty> : IThenIncludableQuerySet<TModel, TPreviousProperty> where TModel : class where TPreviousProperty : class
  {
    private IEnumerable<TModel> data;

    public InMemoryThenIncludableQuerySet(IEnumerable<TModel> data)
    {
        this.data = data;
    }

    public Task<bool> AnyAsync(Expression<Func<TModel, bool>> clause)
    {
      return Task.FromResult(this.data.Any(clause.Compile()));
    }

    public IEnumerator<TModel> GetEnumerator()
    {
      return this.data.GetEnumerator();
    }

    public IThenIncludableQuerySet<TModel, TProperty> Include<TProperty>(Expression<Func<TModel, TProperty>> include) where TProperty : class
    {
      return new InMemoryThenIncludableQuerySet<TModel, TProperty>(this.data);
    }

    public Task<TModel> SingleAsync(Expression<Func<TModel, bool>> clause)
    {
      return Task.FromResult(this.data.Single(clause.Compile()));
    }

    public Task<TModel> SingleOrDefaultAsync(Expression<Func<TModel, bool>> clause)
    {
      return Task.FromResult(this.data.SingleOrDefault(clause.Compile()));
    }

    public IThenIncludableQuerySet<TModel, TProperty> ThenInclude<TProperty>(Expression<Func<TPreviousProperty, TProperty>> include) where TProperty : class
    {
      return new InMemoryThenIncludableQuerySet<TModel, TProperty>(this.data);
    }

    public Task<List<TModel>> ToListAsync()
    {
      return Task.FromResult(this.data.ToList());
    }

    public IQuerySet<TModel> Where(Expression<Func<TModel, bool>> clause)
    {
      return new InMemoryQuerySet<TModel>(this.data.Where(clause.Compile()));
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.data.GetEnumerator();
    }
  }
}