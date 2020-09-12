using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BudgetSquirrel.Data.EntityFramework.Infrastructure
{
  public class EFQuerySet<TEntity> : IQuerySet<TEntity> where TEntity : class
  {
    private IQueryable<TEntity> queryable;

    public EFQuerySet(IQueryable<TEntity> queryable)
    {
      this.queryable = queryable;
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> clause)
    {
      return this.queryable.AnyAsync(clause);
    }

    public IEnumerator<TEntity> GetEnumerator()
    {
      return this.queryable.GetEnumerator();
    }

    public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> clause)
    {
      return this.queryable.SingleOrDefaultAsync(clause);
    }

    public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> clause)
    {
      return this.queryable.SingleAsync(clause);
    }

    public Task<List<TEntity>> ToListAsync()
    {
      return this.queryable.ToListAsync();
    }

    public IQuerySet<TEntity> Where(Expression<Func<TEntity, bool>> clause)
    {
      return new EFQuerySet<TEntity>(this.queryable.Where(clause));
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.queryable.GetEnumerator();
    }
  }
}