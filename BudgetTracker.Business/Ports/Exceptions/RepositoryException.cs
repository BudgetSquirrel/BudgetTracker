using System;

namespace BudgetTracker.Business.Ports.Exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string message) : base(message) { }
    }
}
