using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Business.Tracking;

namespace BudgetSquirrel.Business.BudgetPlanning
{
  public class UserRootBudgetRelationship
  {
    public User User { get; private set; }
    public Budget RootBudget { get; private set; }
    public BudgetPeriod FirstPeriod { get; private set; }

    public UserRootBudgetRelationship(User user, Budget rootBudget, BudgetPeriod firstPeriod)
    {
      this.User = user;
      this.RootBudget = rootBudget;
      this.FirstPeriod = firstPeriod;
    }
  }
}