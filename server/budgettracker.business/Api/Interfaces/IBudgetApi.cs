using budgettracker.business.Api.Contracts.BudgetApi;
using System.Threading.Tasks;

namespace budgettracker.business.Api.Interfaces
{
    public interface IBudgetApi
    {
        Task<BudgetResponseContract> CreateBudget(BudgetResquestContract budget);        
    }
}
