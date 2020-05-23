using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.TestUtils.Auth;
using Moq;
using Xunit;

namespace BudgetSquirrel.Business.Tests.BudgetPlanning
{
  public class EditRootBudgetCommandTest
  {
    private BuilderFactoryFixture buildersAndFactories;
    private TestServices services;

    public EditRootBudgetCommandTest()
    {
      this.buildersAndFactories = new BuilderFactoryFixture();
      this.services = new TestServices();
    }

    [Fact]
    public async Task Test_NameIsCorrect_WhenNameChanged()
    {
      Mock<IAsyncQueryService> asyncQueryService = new Mock<IAsyncQueryService>();
      UserFactory userFactory = this.buildersAndFactories.GetService<UserFactory>();

      string expectedNewName = "New Name";

      Budget rootBudget = this.buildersAndFactories.BudgetBuilder.SetName("Test Budget").Build();
      User user = userFactory.NewUser(rootBudget.UserId);
      IQueryable<Budget> budgets = new List<Budget>() { rootBudget }.AsQueryable();

      asyncQueryService.Setup(s => s.SingleOrDefaultAsync(budgets, It.IsAny<Expression<Func<Budget, bool>>>()))
                       .Returns(Task.FromResult(rootBudget));
      asyncQueryService.Setup(s => s.SaveChangesAsync()).Returns(Task.CompletedTask);

      EditRootBudgetCommand command = new EditRootBudgetCommand(asyncQueryService.Object, budgets, rootBudget.Id, user, expectedNewName, null);
      await command.Run();

      Assert.Equal(expectedNewName, rootBudget.Name);
    }

    [Fact]
    public async Task Test_SetAmountIsCorrect_WhenAmountChanged()
    {
      Mock<IAsyncQueryService> asyncQueryService = new Mock<IAsyncQueryService>();
      UserFactory userFactory = this.buildersAndFactories.GetService<UserFactory>();

      decimal expectedAmount = (decimal)5.67;

      Budget rootBudget = this.buildersAndFactories.BudgetBuilder.SetFixedAmount(4).Build();
      User user = userFactory.NewUser(rootBudget.UserId);
      IQueryable<Budget> budgets = new List<Budget>() { rootBudget }.AsQueryable();

      asyncQueryService.Setup(s => s.SingleOrDefaultAsync(budgets, It.IsAny<Expression<Func<Budget, bool>>>()))
                       .Returns(Task.FromResult(rootBudget));
      asyncQueryService.Setup(s => s.SaveChangesAsync()).Returns(Task.CompletedTask);

      EditRootBudgetCommand command = new EditRootBudgetCommand(asyncQueryService.Object, budgets, rootBudget.Id, user, null, expectedAmount);
      await command.Run();

      Assert.Equal(expectedAmount, rootBudget.SetAmount);
    }

    [Fact]
    public async Task Test_ThrowsInvalidOperation_WhenUserIsNotOwner()
    {
      Mock<IAsyncQueryService> asyncQueryService = new Mock<IAsyncQueryService>();
      UserFactory userFactory = this.buildersAndFactories.GetService<UserFactory>();

      Guid correctUserId = Guid.NewGuid();
      Guid wrongUserId = Guid.NewGuid();
      User wrongUser = userFactory.NewUser(wrongUserId);

      Budget rootBudget = this.buildersAndFactories.BudgetBuilder.SetOwner(correctUserId).Build();
      IQueryable<Budget> budgets = new List<Budget>() { rootBudget }.AsQueryable();

      asyncQueryService.Setup(s => s.SingleOrDefaultAsync(budgets, It.IsAny<Expression<Func<Budget, bool>>>()))
                       .Returns(Task.FromResult(rootBudget));
      asyncQueryService.Setup(s => s.SaveChangesAsync()).Returns(Task.CompletedTask);

      EditRootBudgetCommand command = new EditRootBudgetCommand(asyncQueryService.Object, budgets, rootBudget.Id, wrongUser, "Test", null);
      await Assert.ThrowsAsync<InvalidOperationException>(() => command.Run());
    }
  }
}