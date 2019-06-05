using budgettracker.business.Api.Contracts.Responses;
using budgettracker.business.Api.Contracts.Requests;

using System.Threading.Tasks;

namespace budgettracker.business.Api.Interfaces
{
    public interface IBudgetApi
    {
        Task<ApiResponse> CreateBudget(ApiRequest request);
    }
}