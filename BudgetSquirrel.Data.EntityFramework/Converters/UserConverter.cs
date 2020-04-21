using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Data.EntityFramework.Models;

namespace BudgetSquirrel.Data.EntityFramework.Converters
{
  public static class UserConverter
  {
    public static User ToDomainModel(UserRecord userData)
    {
      User user = new User(userData.Id,
                            userData.Username,
                            userData.FirstName,
                            userData.LastName,
                            userData.Email);
      return user;
    }
  
  
    public static UserRecord ToDataModel(User userDomain)
    {
        return new UserRecord()
        {
            FirstName = userDomain.FirstName,
            LastName = userDomain.LastName,
            Username = userDomain.Username,
            Id = userDomain.Id,
            Email = userDomain.Email
        };
    }
  }
}