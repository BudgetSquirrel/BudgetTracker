using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BudgetSquirrel.Business
{
  public interface IAsyncQueryService
  {
    Task<T> SingleOrDefaultAsync<T>(IQueryable<T> source, Expression<Func<T, bool>> predicate);
    Task<List<T>> ToListAsync<T>(IQueryable<T> source);
    IQueryable<T> Include<T, TProperty>(IQueryable<T> source, Expression<Func<T, TProperty>> include) where T : class;
    Task<bool> AnyAsync<T>(IQueryable<T> source, Expression<Func<T, bool>> predicate);
  }
}