using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Infrastructure;

namespace BudgetSquirrel.TestUtils.Infrastructure
{
  public class InMemoryQuerySet<TModel> : IQuerySet<TModel> where TModel : class
  {
    private IEnumerable<TModel> data;

    public InMemoryQuerySet(IEnumerable<TModel> data)
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

    public Task<TModel> SingleAsync(Expression<Func<TModel, bool>> clause)
    {
      return Task.FromResult(this.data.Single(clause.Compile()));
    }

    public Task<TModel> SingleOrDefaultAsync(Expression<Func<TModel, bool>> clause)
    {
      return Task.FromResult(this.data.SingleOrDefault(clause.Compile()));
    }

    public Task<List<TModel>> ToListAsync()
    {
      return Task.FromResult(this.data.ToList());
    }

    public IQueryable<TModel> AsQueryable()
    {
      return this.data.AsQueryable();
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