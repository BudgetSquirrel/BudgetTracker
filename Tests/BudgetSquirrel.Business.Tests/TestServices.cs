using System;
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
        services.AddTransient<FakeDbContext>();
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