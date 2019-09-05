using System;

namespace BudgetTracker.Common.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message)
            : base (message)
        {
        }
    }
}
