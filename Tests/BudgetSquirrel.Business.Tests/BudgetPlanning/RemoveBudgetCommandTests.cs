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
  public class RemoveBudgetCommandTests
  {
    private BuilderFactoryFixture buildersAndFactories;
    private TestServices services;

    public RemoveBudgetCommandTests()
    {
      this.buildersAndFactories = new BuilderFactoryFixture();
      this.services = new TestServices();
    }

    [Fact]
    public async Task Test_BudgetNoLongerExists_WhenRemoveCommandCalled()
    {
      Mock<IAsyncQueryService> asyncQueryService = new Mock<IAsyncQueryService>();
      Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
      Mock<IRepository<Budget>> budgetRepo = new Mock<IRepository<Budget>>();UserFactory userFactory = this.buildersAndFactories.GetService<UserFactory>();

      Budget rootBudget = this.buildersAndFactories.BudgetBuilder.SetName("Test Budget").Build();
      User user = userFactory.NewUser(rootBudget.UserId);
      IQueryable<Budget> budgets = new List<Budget>() { rootBudget }.AsQueryable();

      asyncQueryService.Setup(s => s.SingleOrDefaultAsync(budgets, It.IsAny<Expression<Func<Budget, bool>>>()))
                       .Returns(Task.FromResult(rootBudget));
      budgetRepo.Setup(r => r.GetAll()).Returns(budgets);
      unitOfWork.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);
      unitOfWork.Setup(u => u.GetRepository<Budget>()).Returns(budgetRepo.Object);

      RemoveBudgetCommand command = new RemoveBudgetCommand(unitOfWork.Object, asyncQueryService.Object, rootBudget.Id, user);
      await command.Run();

      budgetRepo.Verify(r => r.Remove(rootBudget), Times.Once);
    }

    [Fact]
    public async Task Test_ExceptionThrown_WhenRemoveCommandCalled_ByUserThatDoesntOwnIt()
    {
      Mock<IAsyncQueryService> asyncQueryService = new Mock<IAsyncQueryService>();
      Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
      Mock<IRepository<Budget>> budgetRepo = new Mock<IRepository<Budget>>();UserFactory userFactory = this.buildersAndFactories.GetService<UserFactory>();

      Budget rootBudget = this.buildersAndFactories.BudgetBuilder.SetName("Test Budget").Build();
      User unauthorizedUser = userFactory.NewUser();
      IQueryable<Budget> budgets = new List<Budget>() { rootBudget }.AsQueryable();

      asyncQueryService.Setup(s => s.SingleOrDefaultAsync(budgets, It.IsAny<Expression<Func<Budget, bool>>>()))
                       .Returns(Task.FromResult(rootBudget));
      budgetRepo.Setup(r => r.GetAll()).Returns(budgets);
      unitOfWork.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);
      unitOfWork.Setup(u => u.GetRepository<Budget>()).Returns(budgetRepo.Object);

      RemoveBudgetCommand command = new RemoveBudgetCommand(unitOfWork.Object, asyncQueryService.Object, rootBudget.Id, unauthorizedUser);
      
      await Assert.ThrowsAsync<InvalidOperationException>(() => command.Run());
    }
  }
}