using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using budgettracker.business.Api.Interfaces;
using budgettracker.business.Api.Contracts.BudgetApi;

namespace budgettracker.web.Controllers
{
    [Route("api/budget")]
    public class BudgetApiController: ControllerBase
    {
        private readonly IBudgetApi _budgetApi;

        public BudgetApiController(IBudgetApi budgetApi)
        {
            _budgetApi = budgetApi;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<BudgetResponseContract>> CreateBudget(BudgetResquestContract budget)
        {
            IServiceProvider serviceProvider = HttpContext.RequestServices;
            return await _budgetApi.CreateBudget(budget);
        }

    }
}