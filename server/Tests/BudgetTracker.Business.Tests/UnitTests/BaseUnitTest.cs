using BudgetTracker.Business.Tests;
using System;

namespace BudgetTracker.Business.Tests.UnitTests
{
    public class BaseUnitTest
    {
        private TestStartup _startup;
        private IServiceProvider _services;

        public BaseUnitTest()
        {
            _startup = new TestStartup();
            _services = _startup.Services;
        }

        public E GetService<E>()
        {
            return (E) _services.GetService(typeof(E));
        }
    }
}
