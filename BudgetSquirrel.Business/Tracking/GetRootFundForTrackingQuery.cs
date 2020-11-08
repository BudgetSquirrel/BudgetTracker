using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Business.Infrastructure;

namespace BudgetSquirrel.Business.Tracking
{
  public class GetRootFundForTrackingQuery
  {
    BudgetLoader budgetLoader;
    private IUnitOfWork unitOfWork;

    private Guid userId;
    private DateTime date;

    public GetRootFundForTrackingQuery(
      IUnitOfWork unitOfWork,
      BudgetLoader budgetLoader,
      Guid userId,
      DateTime date)
    {
      this.budgetLoader = budgetLoader;
      this.unitOfWork = unitOfWork;
      this.userId = userId;
      this.date = date;
    }

    public async Task<Fund> Run()
    {
      DateTime now = DateTime.Now.Date;
      IRepository<Fund> fundRepo = this.unitOfWork.GetRepository<Fund>();
      IRepository<BudgetPeriod> periodRepo = this.unitOfWork.GetRepository<BudgetPeriod>();
      Fund rootFund = await fundRepo.GetAll()
                                    .SingleAsync(f => f.UserId == this.userId &&
                                                      !f.ParentFundId.HasValue);
      IQuerySet<BudgetPeriod> usersBudgetPeriods = periodRepo.GetAll().Include(p => p.RootBudget)
                                                            .Where(p => p.RootBudget.FundId == rootFund.Id);
      BudgetPeriod currentPeriod = await BudgetPeriodQueryUtils.GetForDate(usersBudgetPeriods, this.date);

      rootFund = await this.budgetLoader.LoadFundTree(rootFund, currentPeriod);

      return rootFund;
    }
  }
}
