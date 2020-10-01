using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace BudgetSquirrel.Data.EntityFramework.Infrastructure
{
  public class EFThenIncludableQuerySet<TEntity, TPreviousProperty> : IThenIncludableQuerySet<TEntity, TPreviousProperty> where TEntity : class where TPreviousProperty : class
  {
    private IIncludableQueryable<TEntity, TPreviousProperty> queryable;

    public EFThenIncludableQuerySet(IIncludableQueryable<TEntity, TPreviousProperty> queryable)
    {
      this.queryable = queryable;
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> clause)
    {
      return this.queryable.AnyAsync(clause);
    }

    public IThenIncludableQuerySet<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, TProperty>> include) where TProperty : class
    {
      return new EFThenIncludableQuerySet<TEntity, TProperty>(this.queryable.Include(include));
    }

    public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> clause)
    {
      return this.queryable.SingleOrDefaultAsync(clause);
    }

    public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> clause)
    {
      return this.queryable.SingleAsync(clause);
    }

    public IThenIncludableQuerySet<TEntity, TProperty> ThenInclude<TProperty>(Expression<Func<TPreviousProperty, TProperty>> include) where TProperty : class
    {
      return new EFThenIncludableQuerySet<TEntity, TProperty>(this.queryable.ThenInclude(include));
    }

    public Task<List<TEntity>> ToListAsync()
    {
      return this.queryable.ToListAsync();
    }

    public IQueryable<TEntity> AsQueryable()
    {
      return this.queryable;
    }

    public IQuerySet<TEntity> Where(Expression<Func<TEntity, bool>> clause)
    {
      return new EFQuerySet<TEntity>(this.queryable.Where(clause));
    }

    public IEnumerator<TEntity> GetEnumerator()
    {
      return this.queryable.ToList().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.queryable.ToList().GetEnumerator();
    }
  }
}