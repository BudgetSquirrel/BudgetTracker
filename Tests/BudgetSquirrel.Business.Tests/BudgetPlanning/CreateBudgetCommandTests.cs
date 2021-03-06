using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Business.Infrastructure;
using BudgetSquirrel.TestUtils;
using BudgetSquirrel.TestUtils.Auth;
using BudgetSquirrel.TestUtils.Infrastructure;
using Moq;
using Xunit;

namespace BudgetSquirrel.Business.Tests.BudgetPlanning
{
  public class CreateBudgetCommandTests
  {
    private BuilderFactoryFixture buildersAndFactories;
    private TestServices services;

    public CreateBudgetCommandTests()
    {
      this.buildersAndFactories = new BuilderFactoryFixture();
      this.services = new TestServices();
    }

    [Fact]
    public async Task Test_FixedAmountCorrect_WhenFixedAmountSet()
    {
      Budget createdBudget = null;
      decimal setAmount = 49;

      Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
      Mock<IRepository<Budget>> budgetRepo = new Mock<IRepository<Budget>>();
      UserFactory userFactory = this.buildersAndFactories.GetService<UserFactory>();

      Budget rootBudget = this.buildersAndFactories.BudgetBuilder.Build();
      IIncludableQuerySet<Budget> budgets = new InMemoryIncludableQuerySet<Budget>(new List<Budget>() { rootBudget });

      budgetRepo.Setup(r => r.GetAll()).Returns(budgets);
      budgetRepo.Setup(r => r.Add(It.IsAny<Budget>())).Callback((Budget budget) => {
        createdBudget = budget;
      });
      unitOfWork.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);
      unitOfWork.Setup(u => u.GetRepository<Budget>()).Returns(budgetRepo.Object);
      
      CreateBudgetCommand command = new CreateBudgetCommand(unitOfWork.Object, rootBudget.Id, "", setAmount);
      await command.Run();

      Assert.Equal(setAmount, createdBudget.SetAmount);
    }

    [Fact]
    public async Task Test_CorrectParentBudgetSet_WhenSubBudgetCreated()
    {
      Budget createdBudget = null;
      Budget rootBudget = this.buildersAndFactories.BudgetBuilder.Build();

      Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
      Mock<IRepository<Budget>> budgetRepo = new Mock<IRepository<Budget>>();
      UserFactory userFactory = this.buildersAndFactories.GetService<UserFactory>();

      IIncludableQuerySet<Budget> budgets = new InMemoryIncludableQuerySet<Budget>(new List<Budget>() { rootBudget });

      budgetRepo.Setup(r => r.GetAll()).Returns(budgets);
      budgetRepo.Setup(r => r.Add(It.IsAny<Budget>())).Callback((Budget budget) => {
        createdBudget = budget;
      });
      unitOfWork.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);
      unitOfWork.Setup(u => u.GetRepository<Budget>()).Returns(budgetRepo.Object);
      
      CreateBudgetCommand command = new CreateBudgetCommand(unitOfWork.Object, rootBudget.Id, "", 12);
      await command.Run();

      Assert.Equal(rootBudget.Fund.Id, createdBudget.Fund.ParentFundId);
    }

    [Fact]
    public async Task Test_FundBalanceIs0_WhenBudgetCreated()
    {
      Budget createdBudget = null;
      Budget rootBudget = this.buildersAndFactories.BudgetBuilder.Build();
      decimal expectedFundBalance = 0;

      Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
      Mock<IRepository<Budget>> budgetRepo = new Mock<IRepository<Budget>>();
      UserFactory userFactory = this.buildersAndFactories.GetService<UserFactory>();

      IIncludableQuerySet<Budget> budgets = new InMemoryIncludableQuerySet<Budget>(new List<Budget>() { rootBudget });

      budgetRepo.Setup(r => r.GetAll()).Returns(budgets);
      budgetRepo.Setup(r => r.Add(It.IsAny<Budget>())).Callback((Budget budget) => {
        createdBudget = budget;
      });
      unitOfWork.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);
      unitOfWork.Setup(u => u.GetRepository<Budget>()).Returns(budgetRepo.Object);
      
      CreateBudgetCommand command = new CreateBudgetCommand(unitOfWork.Object, rootBudget.Id, "", 12);
      await command.Run();

      Assert.Equal(expectedFundBalance, createdBudget.Fund.FundBalance);
    }
  }
}