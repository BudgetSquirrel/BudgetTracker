using System;
using BudgetSquirrel.TestUtils;
using BudgetSquirrel.TestUtils.Auth;
using BudgetSquirrel.TestUtils.Budgeting;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetSquirrel.TestUtils
{
    public class BuilderFactoryFixture : IDisposable, IServiceProvider
    {
        private ServiceProvider _buildersAndFactories;

        public IBudgetBuilder BudgetBuilder => GetService<IBudgetBuilder>();

        public IFundBuilder FundBuilder => GetService<IFundBuilder>();

        public IBudgetPeriodBuilder BudgetPeriodBuilder => GetService<IBudgetPeriodBuilder>();

        public BuilderFactoryFixture()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _buildersAndFactories = services.BuildServiceProvider();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<BuilderFactoryFixture>(this);
            services.AddTransient<BudgetDurationBuilderProvider>();
            services.AddTransient<IFundBuilder, FundBuilder>();
            services.AddTransient<IBudgetBuilder, BudgetBuilder>();
            services.AddTransient<IBudgetPeriodBuilder, BudgetPeriodBuilder>();
            services.AddTransient<BudgetTreeBuilder>();
            
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