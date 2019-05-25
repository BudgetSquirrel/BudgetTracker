using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using budgettracker.business.Services;
using budgettracker.business.Api.Contracts.BudgetApi;
using System;

namespace budgettracker.web.Controllers
{
    [Route("api/budget")]
    public class BudgetController: Controller
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService )
        {
            _budgetService = budgetService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<BudgetResponseContract>> CreateBudget(BudgetResquestContract budget)
        {
            await _budgetService.CreateBudget(budget);

            return Ok();
        }

    }
}