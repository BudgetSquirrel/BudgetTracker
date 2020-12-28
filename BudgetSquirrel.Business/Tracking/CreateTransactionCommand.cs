using System;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Infrastructure;

namespace BudgetSquirrel.Business.Tracking
{
  public struct CreateTransactionCommandArgs
  {
    public string Summary { get; set; }
    public string Vendor { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string CheckNumber { get; set; }
    public string Notes { get; set; }
    public Guid FundId { get; set; }
  }
  
  public class CreateTransactionCommand
  {
    private IUnitOfWork unitOfWork;
    private FundLoader fundLoader;
    private CreateTransactionCommandArgs args;

    public CreateTransactionCommand(IUnitOfWork unitOfWork, FundLoader fundLoader, CreateTransactionCommandArgs args)
    {
      this.unitOfWork = unitOfWork;
      this.fundLoader = fundLoader;
      this.args = args;
    }

    public async Task Run()
    {
      // TODO: Test this.
      
      IRepository<Transaction> transactionRepository = this.unitOfWork.GetRepository<Transaction>();
      IRepository<Fund> fundRepository = this.unitOfWork.GetRepository<Fund>();
      IRepository<BudgetPeriod> periodRepository = this.unitOfWork.GetRepository<BudgetPeriod>();
      
      Fund fund = await fundRepository.GetAll().Include(f => f.SubFunds).SingleAsync(f => f.Id == this.args.FundId);
      await this.fundLoader.LoadAncestorFunds(fund);
      BudgetPeriod currentPeriod = await BudgetPeriodQueryUtils.GetForDate(
        periodRepository.GetAll()
                        .Include(p => p.RootBudget)
                        .Where(p => p.RootBudget.FundId == fund.Id),
        DateTime.Now);
      
      this.Validate(fund, currentPeriod);

      Transaction transaction = new Transaction(
        this.args.Summary,
        this.args.Vendor,
        this.args.Date,
        this.args.Amount,
        this.args.CheckNumber,
        this.args.Notes,
        this.args.FundId);

      transactionRepository.Add(transaction);
      fund.ApplyTransaction(transaction);
      fundRepository.Update(fund);

      await this.unitOfWork.SaveChangesAsync();
    }

    private void Validate(Fund fund, BudgetPeriod currentPeriod)
    {
      if (!currentPeriod.IsPeriodForDate(this.args.Date))
      {
        throw new CommandException($"Cannot create a transaction for a past budget period");
      }
      if (fund.SubFunds.Any())
      {
        throw new CommandException($"Cannot create a transaction for a fund that is a category fund (has sub-funds)");
      }
    }
  }
}