using budgettracker.business.Api.Contracts.BudgetApi;
using System.Threading.Tasks;

namespace budgettracker.business.Services
{
    public interface IBudgetService
    {

        Task CreateBudget(BudgetResquestContract budget);        

    }
}
