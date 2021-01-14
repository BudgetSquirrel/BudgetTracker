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
  public class FinalizeBudgetCommandTest
  {
    private BuilderFactoryFixture buildersAndFactories;
    private TestServices services;

    public FinalizeBudgetCommandTest()
    {
      this.buildersAndFactories = new BuilderFactoryFixture();
      this.services = new TestServices();
    }

    [Fact]
    public async Task Test_FinalizeBudget_ThrowsError_WhenBudgetNotFullyAllocated()
    {
      User user = this.buildersAndFactories.GetService<UserFactory>().NewUser();

      (Budget rootBudget, IEnumerable<Budget> allSubBudgetsFlat) = this.buildersAndFactories.GetService<BudgetTreeBuilder>()
        .SetOwner(user)
        .SetFixedAmount(300)
        .AddSubBudget(a => a.SetFixedAmount(100)
                            .AddSubBudget(aa => aa.SetFixedAmount(50)))
        .AddSubBudget(b => b.SetFixedAmount(200)
                            .AddSubBudget(ba => ba.SetFixedAmount(75))
                            .AddSubBudget(bb => bb.SetFixedAmount(125)))
        .BuildTree();

      IUnitOfWork unitOfWork = this.services.GetService<IUnitOfWork>();
      FundLoader budgetLoader = this.services.GetService<FundLoader>();

      var budgetRepository = unitOfWork.GetRepository<Budget>();
      var fundRepository = unitOfWork.GetRepository<Fund>();

      budgetRepository.Add(rootBudget);
      fundRepository.Add(rootBudget.Fund);
      foreach (Budget subBudget in allSubBudgetsFlat)
      {
          budgetRepository.Add(subBudget);
          fundRepository.Add(subBudget.Fund);
      }
      var userRepository = unitOfWork.GetRepository<User>();
      userRepository.Add(rootBudget.Fund.User);
      var periodRepository = unitOfWork.GetRepository<BudgetPeriod>();
      periodRepository.Add(rootBudget.BudgetPeriod);
      var durationRepository = unitOfWork.GetRepository<BudgetDurationBase>();
      durationRepository.Add(rootBudget.Fund.Duration);

      FinalizeBudgetPeriodCommand command = new FinalizeBudgetPeriodCommand(unitOfWork, budgetLoader, rootBudget.Id, rootBudget.Fund.User);
      await Assert.ThrowsAsync<InvalidOperationException>(() => command.Run());
    }

    [Fact]
    public async Task Test_FinalizeBudget_Succeeds_WhenBudgetsValid()
    {
      User user = this.buildersAndFactories.GetService<UserFactory>().NewUser();

      (Budget rootBudget, IEnumerable<Budget> allSubBudgetsFlat) = this.buildersAndFactories.GetService<BudgetTreeBuilder>()
        .SetOwner(user)
        .SetFixedAmount(300)
        .AddSubBudget(a => a.SetFixedAmount(100)
                            .AddSubBudget(aa => aa.SetFixedAmount(100)))
        .AddSubBudget(b => b.SetFixedAmount(200)
                            .AddSubBudget(ba => ba.SetFixedAmount(75))
                            .AddSubBudget(bb => bb.SetFixedAmount(125)))
        .BuildTree();

      IUnitOfWork unitOfWork = this.services.GetService<IUnitOfWork>();
      FundLoader budgetLoader = this.services.GetService<FundLoader>();

      var budgetRepository = unitOfWork.GetRepository<Budget>();
      var fundRepository = unitOfWork.GetRepository<Fund>();

      budgetRepository.Add(rootBudget);
      fundRepository.Add(rootBudget.Fund);
      foreach (Budget subBudget in allSubBudgetsFlat)
      {
          budgetRepository.Add(subBudget);
          fundRepository.Add(subBudget.Fund);
      }

      var userRepository = unitOfWork.GetRepository<User>();
      userRepository.Add(rootBudget.Fund.User);
      var periodRepository = unitOfWork.GetRepository<BudgetPeriod>();
      periodRepository.Add(rootBudget.BudgetPeriod);
      var durationRepository = unitOfWork.GetRepository<BudgetDurationBase>();
      durationRepository.Add(rootBudget.Fund.Duration);

      FinalizeBudgetPeriodCommand command = new FinalizeBudgetPeriodCommand(unitOfWork, budgetLoader, rootBudget.Id, rootBudget.Fund.User);
      await command.Run();

      Assert.NotNull(rootBudget.DateFinalizedTo);
    }
  }
}