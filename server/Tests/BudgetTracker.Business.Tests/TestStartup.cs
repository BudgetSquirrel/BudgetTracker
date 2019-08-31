using BudgetTracker.Business.Budgeting;
using BudgetTracker.TestUtils.Budgeting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BudgetTracker.Business.Tests
{
    public class TestStartup
    {
        private TestStartup _singleton;
        private IServiceProvider _services;

        public TestStartup Instance
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
        }
    }
}
