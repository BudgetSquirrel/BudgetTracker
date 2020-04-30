using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Data.EntityFramework.Models;

namespace BudgetSquirrel.Data.EntityFramework.Converters
{
  public static class BudgetConverter
  {
    public static BudgetRecord ToDataModel(Budget budget)
    {
      BudgetRecord record = new BudgetRecord()
      {
        Name = budget.Name,
        FundBalance = budget.FundBalance,
        PercentAmount = budget.PercentAmount,
        SetAmount = budget.SetAmount,
        BudgetStart = budget.BudgetStart
      };
      return record;
    }

    public static Budget ToDomainModel(BudgetRecord record)
    {
      Budget budget = new Budget(record.Name, record.FundBalance, null, record.BudgetStart);
      return budget;
    }
  }
}