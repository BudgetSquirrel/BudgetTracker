using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BudgetSquirrel.Business
{
  public interface IAsyncQueryService
  {
    Task<T> SingleOrDefaultAsync<T>(IQueryable<T> source, Expression<Func<T, bool>> predicate);
    IQueryable<T> Include<T, TProperty>(IQueryable<T> source, Expression<Func<T, TProperty>> include) where T : class;
    Task SaveChangesAsync();
  }
}