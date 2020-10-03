using System;
using BudgetSquirrel.TestUtils;
using BudgetSquirrel.TestUtils.Auth;
using BudgetSquirrel.TestUtils.Budgeting;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetSquirrel.Business.Tests
{
    public class BuilderFactoryFixture : IDisposable, IServiceProvider
    {
        private ServiceProvider _buildersAndFactories;

        public IBudgetBuilder BudgetBuilder => GetService<IBudgetBuilder>();

        public IFundBuilder FundBuilder => GetService<IFundBuilder>();

        public BuilderFactoryFixture()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _buildersAndFactories = services.BuildServiceProvider();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<BudgetDurationBuilderProvider>();
            services.AddTransient<IFundBuilder, FundBuilder>();
            services.AddTransient<IBudgetBuilder, BudgetBuilder>();
            
            services.AddScoped<UserFactory>();
        }

        public void Dispose()
        {
            _buildersAndFactories.Dispose();
        }

        public object GetService(Type serviceType)
        {
            return _buildersAndFactories.GetService(serviceType);
        }

        public T GetService<T>() => (T) GetService(typeof(T));
    }
}