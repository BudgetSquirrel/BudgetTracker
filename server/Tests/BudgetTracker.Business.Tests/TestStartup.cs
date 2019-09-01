using BudgetTracker.Business.Budgeting;
using BudgetTracker.TestUtils.Auth;
using BudgetTracker.TestUtils.Budgeting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BudgetTracker.Business.Tests
{
    public class TestStartup
    {
        private static TestStartup _singleton;
        private IServiceProvider _services;

        /// <summary>
        /// Instantiate the singleton instance of TestStartup.
        /// This is private to enforce the singleton pattern.
        /// </summary>
        private TestStartup()
        {}

        public static TestStartup Instance
        {
            get {
                if (_singleton == null)
                    _singleton = new TestStartup();
                return _singleton;
            }
        }

        public IServiceProvider Services
        {
            get {
                if (_services == null)
                {
                    IServiceCollection serviceBuilder = new ServiceCollection();
                    ConfigureServices(serviceBuilder);
                    _services = serviceBuilder.BuildServiceProvider();
                }
                return _services;
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<BudgetBuilderFactory<Budget>>();
            services.AddScoped<BudgetBuilderFactory<CreateBudgetRequestMessage>>();
            services.AddScoped<UserFactory>();
        }
    }
}
