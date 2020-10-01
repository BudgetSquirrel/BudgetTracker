using System;
using BudgetSquirrel.Business.Tracking;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.Business.Auth
{
  public class CreateUserCommand
  {
    private const string newUserRootFundName = "My Budget";
    private const decimal newUserRootFundBalance = 0;
    private const int newUserRootBudgetDurationEndDate = 31;
    private const bool newUserRootBudgetDurationShouldRollover = false;

    private string userName;
    private string firstName;
    private string lastName;
    private string email;

    public CreateUserCommand(string username, string firstName, string lastName, string email)
    {
      this.userName = username;
      this.firstName = firstName;
      this.lastName = lastName;
      this.email = email;
    }

    public UserRootBudgetRelationship Run()
    {
      User user = new User(this.userName,
                            this.firstName,
                            this.lastName,
                            this.email);
      (Budget rootBudget, BudgetPeriod firstPeriod) budgetWithPeriod = CreateNewUserRootBudget(user);
      UserRootBudgetRelationship userWithBudget = new UserRootBudgetRelationship(user, budgetWithPeriod.rootBudget, budgetWithPeriod.firstPeriod);
      return userWithBudget;
    }

    private (Budget, BudgetPeriod) CreateNewUserRootBudget(User user)
    {
      BudgetDurationBase duration = new MonthlyBookEndedDuration(newUserRootBudgetDurationEndDate, newUserRootBudgetDurationShouldRollover);
      Fund rootFund = new Fund(newUserRootFundName, newUserRootFundBalance, duration, user.Id);
      DateTime startDate = DateTime.Now;
      DateTime endDate = duration.GetEndDateFromStartDate(startDate);
      BudgetPeriod firstBudgetPeriod = new BudgetPeriod(startDate, endDate);
      Budget firstRootBudget = new Budget(rootFund, firstBudgetPeriod);

      return (firstRootBudget, firstBudgetPeriod);
    }
  }
}