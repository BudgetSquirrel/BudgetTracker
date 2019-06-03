using budgettracker.business.Api.Contracts.Responses;
using budgettracker.business.Api.Contracts.Requests;
using budgettracker.business.Api.Interfaces;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace budgettracker.web.Controllers
{
    [Route("api/budget")]
    [ApiController]
    public class BudgetApiController : ControllerBase
    {
        
        private readonly IBudgetApi _budgetApi;

        public BudgetApiController(IBudgetApi budgetApi)
        {
            _budgetApi = budgetApi;
        }

        [HttpPost("create")]
        public async Task<ApiResponse> CreateBudget(ApiRequest request)
        {
            return await _budgetApi.CreateBudget(request);
        }
    }
}