using System;

namespace BudgetSquirrel.Data.EntityFramework.Models
{
  public class UserRecord
  {
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
  }
}