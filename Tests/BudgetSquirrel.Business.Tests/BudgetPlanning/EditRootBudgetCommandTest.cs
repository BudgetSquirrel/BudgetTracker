using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Business.Infrastructure;
using BudgetSquirrel.TestUtils;
using BudgetSquirrel.TestUtils.Auth;
using BudgetSquirrel.TestUtils.Infrastructure;
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
      Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
      Mock<IRepository<Budget>> budgetRepo = new Mock<IRepository<Budget>>();
      UserFactory userFactory = this.buildersAndFactories.GetService<UserFactory>();

      string expectedNewName = "New Name";

      Budget rootBudget = this.buildersAndFactories.BudgetBuilder.Build();
      User user = userFactory.NewUser(rootBudget.Fund.UserId);
      IIncludableQuerySet<Budget> budgets = new InMemoryIncludableQuerySet<Budget>(new List<Budget>() { rootBudget });

      budgetRepo.Setup(r => r.GetAll()).Returns(budgets);
      unitOfWork.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);
      unitOfWork.Setup(u => u.GetRepository<Budget>()).Returns(budgetRepo.Object);

      EditRootBudgetCommand command = new EditRootBudgetCommand(unitOfWork.Object, rootBudget.Id, user, expectedNewName, null);
      await command.Run();

      Assert.Equal(expectedNewName, rootBudget.Fund.Name);
    }

    [Fact]
    public async Task Test_SetAmountIsCorrect_WhenAmountChanged()
    {
      Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
      Mock<IRepository<Budget>> budgetRepo = new Mock<IRepository<Budget>>();
      UserFactory userFactory = this.buildersAndFactories.GetService<UserFactory>();

      decimal expectedAmount = (decimal)5.67;

      Budget rootBudget = this.buildersAndFactories.BudgetBuilder.SetFixedAmount(4).Build();
      User user = userFactory.NewUser(rootBudget.Fund.UserId);
      IIncludableQuerySet<Budget> budgets = new InMemoryIncludableQuerySet<Budget>(new List<Budget>() { rootBudget });

      budgetRepo.Setup(r => r.GetAll()).Returns(budgets);
      unitOfWork.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);
      unitOfWork.Setup(u => u.GetRepository<Budget>()).Returns(budgetRepo.Object);

      EditRootBudgetCommand command = new EditRootBudgetCommand(unitOfWork.Object, rootBudget.Id, user, null, expectedAmount);
      await command.Run();

      Assert.Equal(expectedAmount, rootBudget.SetAmount);
    }

    [Fact]
    public async Task Test_ThrowsInvalidOperation_WhenUserIsNotOwner()
    {
      Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
      Mock<IRepository<Budget>> budgetRepo = new Mock<IRepository<Budget>>();
      UserFactory userFactory = this.buildersAndFactories.GetService<UserFactory>();

      Guid correctUserId = Guid.NewGuid();
      Guid wrongUserId = Guid.NewGuid();
      User wrongUser = userFactory.NewUser(wrongUserId);

      Budget rootBudget = this.buildersAndFactories.BudgetBuilder
                                                   .SetFund(f =>
                                                      f.SetOwner(correctUserId))
                                                   .Build();
      IIncludableQuerySet<Budget> budgets = new InMemoryIncludableQuerySet<Budget>(new List<Budget>() { rootBudget });

      budgetRepo.Setup(r => r.GetAll()).Returns(budgets);
      unitOfWork.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);
      unitOfWork.Setup(u => u.GetRepository<Budget>()).Returns(budgetRepo.Object);

      EditRootBudgetCommand command = new EditRootBudgetCommand(unitOfWork.Object, rootBudget.Id, wrongUser, "Test", null);
      await Assert.ThrowsAsync<InvalidOperationException>(() => command.Run());
    }
  }
}