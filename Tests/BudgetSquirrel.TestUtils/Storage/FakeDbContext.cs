using System.Collections.Generic;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.TestUtils.Storage
{
  public class FakeDbContext
  {
    public List<User> Users { get; private set; } = new List<User>();
    public List<BudgetDurationBase> BudgetDurations { get; private set; } = new List<BudgetDurationBase>();
    public List<MonthlyBookEndedDuration> MonthlyBookEndedDurations { get; private set; } = new List<MonthlyBookEndedDuration>();
    public List<DaySpanDuration> DaySpanDurations { get; private set; } = new List<DaySpanDuration>();
    public List<Budget> Budgets { get; private set; } = new List<Budget>();
  }
}