using System;
using Bogus;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.TestUtils.Budgeting;

namespace BudgetSquirrel.TestUtils
{
  public class FundBuilder : IFundBuilder
  {
    private Faker _faker = new Faker();
    private BudgetDurationBuilderProvider _budgetDurationBuilderProvider;

    private Guid Id;

    private Guid _ownerId;

    private Fund _parentFund;

    private string _name;

    private decimal _fundBalance;

    private IBudgetDurationBuilder _durationBuilder;

    public FundBuilder(BudgetDurationBuilderProvider budgetDurationBuilderProvider)
    {
        _budgetDurationBuilderProvider = budgetDurationBuilderProvider;
        InitRandomized();
    }

    private void InitRandomized()
    {
        Id = Guid.NewGuid();
        _ownerId = Guid.NewGuid();
        _name = _faker.Lorem.Word();
        
        InitRandomDuration();
    }

    private void InitRandomDuration(bool? isDaySpanDuration=null)
    {
        isDaySpanDuration = isDaySpanDuration ?? _faker.Random.Bool();

        if (isDaySpanDuration == true)
        {
            _durationBuilder = _budgetDurationBuilderProvider.GetBuilder<DaySpanDuration>();
        }
        else
        {
            _durationBuilder = _budgetDurationBuilderProvider.GetBuilder<MonthlyBookEndedDuration>();
        }
    }

    public IFundBuilder SetParentFund(Fund parentFund)
    {
        _parentFund = parentFund;
        return this;
    }

    public IFundBuilder SetOwner(Guid userId)
    {
        _ownerId = userId;
        return this;
    }

    public IFundBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public IFundBuilder SetFundBalance(decimal fundBalance) {
        _fundBalance = fundBalance;
        return this;
    }

    public IFundBuilder SetDurationEndDayOfMonth(int value)
    {
        if (_durationBuilder == null || !(_durationBuilder is MonthlyBookEndedDurationBuilder))
            InitRandomDuration(false);
        ((MonthlyBookEndedDurationBuilder) _durationBuilder).SetDurationEndDayOfMonth(value);
        return this;
    }

    public IFundBuilder SetDurationRolloverEndDateOnSmallMonths(bool value)
    {
        if (_durationBuilder == null || !(_durationBuilder is MonthlyBookEndedDurationBuilder))
            InitRandomDuration(false);
        ((MonthlyBookEndedDurationBuilder) _durationBuilder).SetDurationRolloverEndDateOnSmallMonths(value);
        return this;
    }

    public IFundBuilder SetDurationNumberDays(int value)
    {
        if (_durationBuilder == null || !(_durationBuilder is DaySpanDurationBuilder))
            InitRandomDuration(true);
        ((DaySpanDurationBuilder) _durationBuilder).SetNumberDays(value);
        return this;
    }

    public Fund Build()
    {
        Fund fund = null;
        if (_parentFund != null)
        {
            fund = new Fund(_parentFund, _name, _fundBalance);
            fund.ParentFund = _parentFund;
        }
        else
        {
            BudgetDurationBase budgetDuration = _durationBuilder.Build();
            fund = new Fund(
                Guid.NewGuid(),
                _name,
                _fundBalance,
                budgetDuration,
                _ownerId
            );
        }
        
        return fund;
    }
  }
}