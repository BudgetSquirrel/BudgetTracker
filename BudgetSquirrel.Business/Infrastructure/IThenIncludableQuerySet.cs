using System;
using System.Linq.Expressions;

namespace BudgetSquirrel.Business.Infrastructure
{
  public interface IThenIncludableQuerySet<TEntity, TPreviousProperty> : IIncludableQuerySet<TEntity> where TEntity : class where TPreviousProperty : class
  {
    IThenIncludableQuerySet<TEntity, TProperty> ThenInclude<TProperty>(Expression<Func<TPreviousProperty, TProperty>> include) where TProperty : class;
  }
}