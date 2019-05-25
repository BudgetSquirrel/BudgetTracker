using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using budgettracker.business.Services;
using budgettracker.common.Models;

namespace budgettracker.web.Controllers
{
    [Route("api/budget")]
    public class BudgetController: Controller
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Budget>> CreateBudget(Budget budget)
        {
            await _budgetService.CreateBudget(budget);

            return Ok();
        }

    }
}