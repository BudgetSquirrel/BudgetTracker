using System;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.TestUtils;
using Xunit;

namespace BudgetSquirrel.Business.Tests.Auth
{
  public class CreateUserCommandTests : IDisposable
  {
    private BuilderFactoryFixture _builderFactoryFixture;
    private TestServices _services;

    public CreateUserCommandTests()
    {
        _builderFactoryFixture = new BuilderFactoryFixture();
        _services = new TestServices();
    }

    [Fact]
    public void Test_CreateUser_SetsPropertiesCorrectly()
    {
      string firstName = "Ian";
      string lastName = "Kirkpatrick";
      string email = "ianmann56@gmail.com";
      string username = "ianmann56";

      CreateUserCommand command = new CreateUserCommand(username, firstName, lastName, email);
      UserRootBudgetRelationship userRootBudgetRelationship = command.Run();
      Assert.Equal(userRootBudgetRelationship.User.FirstName, firstName);
      Assert.Equal(userRootBudgetRelationship.User.LastName, lastName);
      Assert.Equal(userRootBudgetRelationship.User.Email, email);
      Assert.Equal(userRootBudgetRelationship.User.Username, username);
    }

    [Fact]
    public void Test_CreateUser_CreatesRootBudget()
    {
      string firstName = "Ian";
      string lastName = "Kirkpatrick";
      string email = "ianmann56@gmail.com";
      string username = "ianmann56";

      CreateUserCommand command = new CreateUserCommand(username, firstName, lastName, email);
      UserRootBudgetRelationship userRootBudgetRelationship = command.Run();
      
      Assert.NotNull(userRootBudgetRelationship.RootBudget);
    }

    [Fact]
    public void Test_CreateUser_SetsRootBudgetBalanceTo0()
    {
      string firstName = "Ian";
      string lastName = "Kirkpatrick";
      string email = "ianmann56@gmail.com";
      string username = "ianmann56";

      decimal expectedFundBalance = 0;

      CreateUserCommand command = new CreateUserCommand(username, firstName, lastName, email);
      UserRootBudgetRelationship userRootBudgetRelationship = command.Run();
      
      Assert.Equal(userRootBudgetRelationship.RootBudget.Fund.FundBalance, expectedFundBalance);
    }

    [Fact]
    public void Test_CreateUser_SetsRootBudgetSetAmountTo0()
    {
      string firstName = "Ian";
      string lastName = "Kirkpatrick";
      string email = "ianmann56@gmail.com";
      string username = "ianmann56";

      decimal expectedSetAmount = 0;

      CreateUserCommand command = new CreateUserCommand(username, firstName, lastName, email);
      UserRootBudgetRelationship userRootBudgetRelationship = command.Run();
      
      Assert.Equal(userRootBudgetRelationship.RootBudget.SetAmount, expectedSetAmount);
    }

    [Fact]
    public void Test_CreateUser_SetsRootBudgetDurationToMonthlyBookEndedDuration()
    {
      string firstName = "Ian";
      string lastName = "Kirkpatrick";
      string email = "ianmann56@gmail.com";
      string username = "ianmann56";

      CreateUserCommand command = new CreateUserCommand(username, firstName, lastName, email);
      UserRootBudgetRelationship userRootBudgetRelationship = command.Run();
      
      Assert.IsAssignableFrom<MonthlyBookEndedDuration>(userRootBudgetRelationship.RootBudget.Fund.Duration);
    }

    [Fact]
    public void Test_CreateUser_SetsRootBudgetDurationToEndOn31st()
    {
      string firstName = "Ian";
      string lastName = "Kirkpatrick";
      string email = "ianmann56@gmail.com";
      string username = "ianmann56";

      int expectedDurationEndDay = 31;

      CreateUserCommand command = new CreateUserCommand(username, firstName, lastName, email);
      UserRootBudgetRelationship userRootBudgetRelationship = command.Run();
      
      MonthlyBookEndedDuration duration = (MonthlyBookEndedDuration) userRootBudgetRelationship.RootBudget.Fund.Duration;
      Assert.Equal(duration.EndDayOfMonth, expectedDurationEndDay);
    }

    [Fact]
    public void Test_CreateUser_SetsRootBudgetDurationRolloverToFalse()
    {
      string firstName = "Ian";
      string lastName = "Kirkpatrick";
      string email = "ianmann56@gmail.com";
      string username = "ianmann56";

      bool expectedDurationRollover = false;

      CreateUserCommand command = new CreateUserCommand(username, firstName, lastName, email);
      UserRootBudgetRelationship userRootBudgetRelationship = command.Run();
      
      MonthlyBookEndedDuration duration = (MonthlyBookEndedDuration) userRootBudgetRelationship.RootBudget.Fund.Duration;
      Assert.Equal(duration.RolloverEndDateOnSmallMonths, expectedDurationRollover);
    }

    public void Dispose()
    {
      _builderFactoryFixture.Dispose();
    }
  }
}