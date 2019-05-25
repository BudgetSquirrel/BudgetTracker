using System.Threading.Tasks;
using budgettracker.common.Models;
using budgettracker.data;

namespace budgettracker.business.Services
{
    public class BudgetService : IBudgetService
    {
        public async Task CreateBudget(Budget budget)
        {
            using (var budgetContext = new BudgetTrackerContext())
            {
                await budgetContext.Budgets.AddAsync(budget);
                budgetContext.SaveChanges();
            }
        }
    }
}