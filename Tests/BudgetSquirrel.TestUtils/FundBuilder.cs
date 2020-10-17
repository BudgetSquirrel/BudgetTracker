using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.TestUtils.Budgeting;

namespace BudgetSquirrel.TestUtils
{
  public class FundBuilder : IFundBuilder
  {
    private Faker _faker = new Faker();
    private BudgetDurationBuilderProvider _budgetDurationBuilderProvider;

    private Guid _id;

    private Guid _ownerId;

    private User _owner;

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
        _id = Guid.NewGuid();
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
        _owner = null;
        return this;
    }

    public IFundBuilder SetOwner(User user)
    {
        _ownerId = user.Id;
        _owner = user;
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
            fund.Duration = _parentFund.Duration;
            fund.User = _parentFund.User;
            fund.ParentFund = _parentFund;
            _parentFund.SubFunds = _parentFund.SubFunds?.Append(fund) ?? new List<Fund>() { fund };
            fund.Id = _id;
        }
        else
        {
            BudgetDurationBase budgetDuration = _durationBuilder.Build();
            fund = new Fund(
                _id,
                _name,
                _fundBalance,
                budgetDuration,
                _ownerId
            );
        }

        if (_owner != null)
        {
            fund.User = _owner;
        }
        
        return fund;
    }
  }
}