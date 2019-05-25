using budgettracker.common.Models;
using System.Threading.Tasks;

namespace budgettracker.business.Services
{
    public interface IBudgetService
    {

        Task CreateBudget(Budget budget);        

    }
}
