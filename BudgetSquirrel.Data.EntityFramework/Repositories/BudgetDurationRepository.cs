using System.Linq;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Data.EntityFramework.Schema;

namespace BudgetSquirrel.Data.EntityFramework.Repositories
{
  public class BudgetDurationRepository : DefaultRepository<BudgetDurationBase>
  {
    private BudgetSquirrelContext context;

    public BudgetDurationRepository(BudgetSquirrelContext context)
      : base(context)
    {
      this.context = context;
    }
    
    public override void Update(BudgetDurationBase instance)
    {
      context.Entry(instance).Property(BudgetDurationSchema.Discriminator).CurrentValue = instance.GetType().Name;
      base.Update(instance);
    }
  }
}