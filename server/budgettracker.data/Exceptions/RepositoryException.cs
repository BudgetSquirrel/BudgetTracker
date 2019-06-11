using System;

namespace budgettracker.data.Exceptions
{
    public class RepositoryException : Exception 
    {
        public RepositoryException(string message) : base(message) { }
    }
}