using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using BudgetSquirrel.Business;

namespace BudgetSquirrel.TestUtils.Storage
{
  public class TestAsyncQueryService : IAsyncQueryService
  {
    private FakeDbContext context;

    public TestAsyncQueryService(FakeDbContext context)
    {
      this.context = context;
    }

    public Task<bool> AnyAsync<T>(IQueryable<T> source, Expression<Func<T, bool>> predicate)
    {
      return Task.FromResult(source.Any(predicate));
    }

    public IQueryable<T> Include<T, TProperty>(IQueryable<T> source, Expression<Func<T, TProperty>> include) where T : class
    {
      return source;
    }

    public Task SaveChangesAsync()
    {
      throw new NotImplementedException();
    }

    public Task<T> SingleOrDefaultAsync<T>(IQueryable<T> source, Expression<Func<T, bool>> predicate)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Returns a string that represents the expression. For example, if the expression
    /// were something like "str => str.Length", this would return the following string:
    ///
    ///   .Lambda #Lambda1<System.Func`2[System.String,System.Int32]>(System.String $str) {
    ///     $str.Length
    ///   }
    /// </summary>
    /// <remarks>
    /// See https://stackoverflow.com/questions/34116591/does-expression-tostring-work
    /// for more information. That's where I got this solution.
    /// </remarks>
    private string ExpressionToString<TEntity, TResult>(Expression<Func<TEntity, TResult>> predicate)
    {
      BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
      PropertyInfo debugViewProp = typeof(Expression).GetProperty("DebugView", flags);
      MethodInfo debugViewGetter = debugViewProp.GetGetMethod(nonPublic: true);
      string debugView = (string)debugViewGetter.Invoke(predicate, null);

      return debugView;
    }
  }
}