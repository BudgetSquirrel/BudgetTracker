using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Business.Infrastructure;
using BudgetSquirrel.Business.Tracking;
using BudgetSquirrel.TestUtils;
using BudgetSquirrel.TestUtils.Budgeting;
using Xunit;

namespace BudgetSquirrel.Business.Tests
{
  public class CreateTransactionCommandTests
  {
    Faker faker = new Faker();
    private BuilderFactoryFixture builderFactoryFixture;
    private TestServices _services;

    public CreateTransactionCommandTests()
    {
        builderFactoryFixture = new BuilderFactoryFixture();
        _services = new TestServices();
    }

    [Fact]
    public async Task Test_FundBalanceUpdated_WhenTransactionCreated()
    {
      IUnitOfWork unitOfWork = this._services.GetService<IUnitOfWork>();

      decimal initialFundBalance = 56;
      decimal expectedNewBalance = 38;
      DateTime transactionDate = this.faker.Date.Recent();

      (Budget budget, IEnumerable<Budget> subBudgets) = this.builderFactoryFixture.GetService<BudgetTreeBuilder>()
                                                                                  .SetFund(f => f.SetFundBalance(initialFundBalance)
                                                                                                 .SetDurationNumberDays(30))
                                                                                  .BuildTree();
      BudgetPeriod period = this.builderFactoryFixture.BudgetPeriodBuilder.SetStartDate(transactionDate.AddDays(-1))
                                                                          .ForRootBudget(budget)
                                                                          .Build();
      unitOfWork.GetRepository<Budget>().Add(budget);
      unitOfWork.GetRepository<Fund>().Add(budget.Fund);
      unitOfWork.GetRepository<BudgetPeriod>().Add(period);
      await unitOfWork.SaveChangesAsync();

      CreateTransactionCommandArgs args = new CreateTransactionCommandArgs()
      {
        Amount = expectedNewBalance - initialFundBalance,
        CheckNumber = this.faker.Random.Number(1000,9999).ToString(),
        Date = this.faker.Date.Recent(),
        FundId = budget.Fund.Id,
        Notes = this.faker.Lorem.Paragraph(),
        Summary = this.faker.Lorem.Sentence(),
        Vendor = this.faker.Company.CompanyName()
      };
      CreateTransactionCommand command = new CreateTransactionCommand(
        unitOfWork,
        this._services.GetService<FundLoader>(),
        args);

      await command.Run();

      IRepository<Fund> fundRepo = unitOfWork.GetRepository<Fund>();
      Fund updatedFund = await fundRepo.GetAll().SingleOrDefaultAsync(f => f.Id == budget.Fund.Id);

      Assert.Equal(expectedNewBalance, updatedFund.FundBalance);
    }

    [Fact]
    public async Task Test_ParentFundBalanceUpdated_WhenTransactionCreatedForSubFund()
    {
      IUnitOfWork unitOfWork = this._services.GetService<IUnitOfWork>();

      decimal initialFundBalance = 56;
      decimal expectedNewBalance = 38;
      DateTime transactionDate = this.faker.Date.Recent();

      (Budget rootBudget, IEnumerable<Budget> subBudgets) = this.builderFactoryFixture.GetService<BudgetTreeBuilder>()
                                                                                  .SetFund(f => f.SetFundBalance(initialFundBalance)
                                                                                                 .SetDurationNumberDays(30))
                                                                                  .AddSubBudget(b => b.SetFund(f => f.SetFundBalance(initialFundBalance)))
                                                                                  .BuildTree();
      BudgetPeriod period = this.builderFactoryFixture.BudgetPeriodBuilder.SetStartDate(transactionDate.AddDays(-1))
                                                                          .ForRootBudget(rootBudget)
                                                                          .Build();
      unitOfWork.GetRepository<Budget>().Add(rootBudget);
      unitOfWork.GetRepository<Budget>().Add(subBudgets.First());
      unitOfWork.GetRepository<Fund>().Add(rootBudget.Fund);
      unitOfWork.GetRepository<Fund>().Add(subBudgets.First().Fund);
      unitOfWork.GetRepository<BudgetPeriod>().Add(period);
      await unitOfWork.SaveChangesAsync();

      CreateTransactionCommandArgs args = new CreateTransactionCommandArgs()
      {
        Amount = expectedNewBalance - initialFundBalance,
        CheckNumber = this.faker.Random.Number(1000,9999).ToString(),
        Date = this.faker.Date.Recent(),
        FundId = subBudgets.First().Fund.Id,
        Notes = this.faker.Lorem.Paragraph(),
        Summary = this.faker.Lorem.Sentence(),
        Vendor = this.faker.Company.CompanyName()
      };
      CreateTransactionCommand command = new CreateTransactionCommand(
        unitOfWork,
        this._services.GetService<FundLoader>(),
        args);

      await command.Run();

      IRepository<Fund> fundRepo = unitOfWork.GetRepository<Fund>();
      Fund updatedRootFund = await fundRepo.GetAll().SingleOrDefaultAsync(f => f.Id == rootBudget.Fund.Id);

      Assert.Equal(expectedNewBalance, updatedRootFund.FundBalance);
    }
  }
}