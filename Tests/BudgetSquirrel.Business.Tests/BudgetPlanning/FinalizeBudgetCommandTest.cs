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

      Budget rootBudget = this.buildersAndFactories.BudgetBuilder
                                               .SetFund(fund => fund.SetOwner(user))
                                               .SetFixedAmount(300)
                                               .Build();

      Budget budgetA = this.buildersAndFactories.BudgetBuilder
                                               .SetFund(fund => fund.SetParentFund(rootBudget.Fund)
                                                                    .SetOwner(user))
                                               .SetFixedAmount(100)
                                               .Build();

      Budget budgetAA = this.buildersAndFactories.BudgetBuilder
                                               .SetFund(fund => fund.SetParentFund(budgetA.Fund)
                                                                    .SetOwner(user))
                                               .SetFixedAmount(50)
                                               .Build();

      Budget budgetB = this.buildersAndFactories.BudgetBuilder
                                               .SetFund(fund => fund.SetParentFund(rootBudget.Fund)
                                                                    .SetOwner(user))
                                               .SetFixedAmount(200)
                                               .Build();

      Budget budgetBA = this.buildersAndFactories.BudgetBuilder
                                               .SetFund(fund => fund.SetParentFund(budgetB.Fund)
                                                                    .SetOwner(user))
                                               .SetFixedAmount(75)
                                               .Build();

      Budget budgetBB = this.buildersAndFactories.BudgetBuilder
                                               .SetFund(fund => fund.SetParentFund(budgetB.Fund)
                                                                    .SetOwner(user))
                                               .SetFixedAmount(125)
                                               .Build();

      IUnitOfWork unitOfWork = this.services.GetService<IUnitOfWork>();
      BudgetLoader budgetLoader = this.services.GetService<BudgetLoader>();

      var budgetRepository = unitOfWork.GetRepository<Budget>();
      budgetRepository.Add(rootBudget);
      budgetRepository.Add(budgetA);
      budgetRepository.Add(budgetAA);
      budgetRepository.Add(budgetB);
      budgetRepository.Add(budgetBA);
      budgetRepository.Add(budgetBB);
      var fundRepository = unitOfWork.GetRepository<Fund>();
      fundRepository.Add(rootBudget.Fund);
      fundRepository.Add(budgetA.Fund);
      fundRepository.Add(budgetAA.Fund);
      fundRepository.Add(budgetB.Fund);
      fundRepository.Add(budgetBA.Fund);
      fundRepository.Add(budgetBB.Fund);
      var userRepository = unitOfWork.GetRepository<User>();
      userRepository.Add(rootBudget.Fund.User);
      var periodRepository = unitOfWork.GetRepository<BudgetPeriod>();
      periodRepository.Add(rootBudget.BudgetPeriod);
      var durationRepository = unitOfWork.GetRepository<BudgetDurationBase>();
      durationRepository.Add(rootBudget.Fund.Duration);

      FinalizeBudgetPeriodCommand command = new FinalizeBudgetPeriodCommand(unitOfWork, budgetLoader, rootBudget.Id, rootBudget.Fund.User);
      await command.Run();
    }
  }
}