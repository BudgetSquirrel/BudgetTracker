using System;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Infrastructure;

namespace BudgetSquirrel.Business
{
  public class BudgetPeriodQueryUtils
  {
    public static Task<BudgetPeriod> GetForDate(IQuerySet<BudgetPeriod> source, DateTime date)
    {
      return source.SingleAsync(p => p.StartDate.Date <= date.Date && p.EndDate.Date >= date.Date);
    }
  }
}