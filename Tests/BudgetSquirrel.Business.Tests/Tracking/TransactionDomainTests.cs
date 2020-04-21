using System;
using Bogus;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Business.Tracking;
using Xunit;

namespace BudgetSquirrel.Business.Tests.Tracking
{
  public class TransactionDomainTests : IDisposable
  {
    private static Faker _faker = new Faker();
    private BuilderFactoryFixture _builderFactoryFixture;

    public TransactionDomainTests()
    {
      _builderFactoryFixture = new BuilderFactoryFixture();
    }

    [Fact]
    public void Test_CanSetAmount()
    {
      Budget budget = _builderFactoryFixture.BudgetBuilder.Build();
      Transaction subject = new Transaction(
        _faker.Company.CompanyName(),
        36,
        _faker.Lorem.Sentence(),
        _faker.Date.Past(),
        _faker.Lorem.Word(),
        _faker.Lorem.Sentence(),
        budget
      );

      subject.SetAmount(27);

      Assert.Equal(27, subject.Amount);
    }

    public void Dispose()
    {
      _builderFactoryFixture.Dispose();
    }
  }
}