using budgettracker.common.Models;
using budgettracker.data.Repositories.Interfaces;

using System.Threading.Tasks;

namespace budgettracker.data.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        public Task CreateBudget(Budget budget)
        {
            // TODO: Save the created budget only
            throw new System.NotImplementedException();
        }
    }
}