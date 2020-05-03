using BudgetSquirrel.Business.Auth;

namespace BudgetSquirrel.Business.BudgetPlanning
{
  public class UserRootBudgetRelationship
  {
    public User User { get; private set; }
    public Budget RootBudget { get; private set; }

    public UserRootBudgetRelationship(User user, Budget rootBudget)
    {
      this.User = user;
      this.RootBudget = rootBudget;
    }
  }
}