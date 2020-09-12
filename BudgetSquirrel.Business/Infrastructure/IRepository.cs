namespace BudgetSquirrel.Business.Infrastructure
{
  public interface IRepository<T> where T : class
  {
    void Add(T instance);

    void Remove(T instance);

    void Update(T instance);

    IIncludableQuerySet<T> GetAll();
  }
}