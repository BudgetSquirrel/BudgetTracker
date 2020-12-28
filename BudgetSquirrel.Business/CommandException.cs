using System;

namespace BudgetSquirrel.Business
{
  public class CommandException : Exception
  {
    public CommandException(string message) : base(message)
    {}
  }
}