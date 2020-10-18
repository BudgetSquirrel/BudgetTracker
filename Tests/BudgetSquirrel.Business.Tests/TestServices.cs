using System;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.TestUtils.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetSquirrel.Business.Tests
{
  public class TestServices : IDisposable, IServiceProvider
  {
    private ServiceProvider _services;

    public TestServices()
    {
        ServiceCollection services = new ServiceCollection();
        ConfigureServices(services);
        _services = services.BuildServiceProvider();
    }

    public void ConfigureServices(IServiceCollection services)
    {
      IUnitOfWork unitOfWork = new InMemoryUnitOfWork();
      services.AddSingleton<IUnitOfWork>(unitOfWork);
      services.AddTransient<BudgetLoader>();
    }
    
    public void Dispose()
    {
      _services.Dispose();
    }

    public object GetService(Type serviceType)
    {
      return _services.GetService(serviceType);
    }

    public T GetService<T>() => (T) GetService(typeof(T));
  }
}