using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BudgetSquirrel.Business.Infrastructure
{
  public interface IQuerySet<T> : IEnumerable<T> where T : class
  {
    Task<bool> AnyAsync(Expression<Func<T, bool>> clause);

    Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> clause);

    Task<T> SingleAsync(Expression<Func<T, bool>> clause);

    Task<List<T>> ToListAsync();

    IQueryable<T> AsQueryable();

    IQuerySet<T> Where(Expression<Func<T, bool>> clause);
  }
}