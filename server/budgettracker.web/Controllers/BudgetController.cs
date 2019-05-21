using Microsoft.AspNetCore.Mvc;
using budgettracker.business.Services;
using System.Threading.Tasks;
using budgettracker.common.Models;

namespace budgettracker.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : Controller
    {
        private readonly IBudgetService budgetService;
        
        public BudgetController(IBudgetService _budgetService) 
        {
            budgetService = _budgetService;
        }

        [HttpPost("createBudget")]
        public async Task<IActionResult> CreateBudget(Budget budget)
        {
            await budgetService.CreateBudget(budget);

            // Will throw exception in service if something fails
            return Ok();
        }


        //    public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        //{
        //    [HttpPost]
        //public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item)
        //    if (todoItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return todoItem;
        //}
    }
}