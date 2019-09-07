using BudgetTracker.BudgetSquirrel.Application.Messages;
using BudgetTracker.BudgetSquirrel.Application;
using GateKeeper.Exceptions;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using System;
using BudgetTracker.BudgetSquirrel.Application.Messages.BudgetApi;

namespace BudgetTracker.BudgetSquirrel.WebApi.Controllers
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
        public async Task<IActionResult> CreateBudget(ApiRequest request)
        {
            try
            {
                return new JsonResult(await _budgetApi.CreateBudget(request));
            }
            catch (AuthenticationException)
            {
                return Forbid();
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteBudgets(ApiRequest request)
        {
            try
            {
                return new JsonResult(await _budgetApi.DeleteBudgets(request));
            }
            catch (AuthenticationException)
            {
                return Forbid();
            }
        }

        [HttpPost("roots")]
        public async Task<IActionResult> GetRootBudget(ApiRequest request)
        {
            try
            {
                return new JsonResult(await _budgetApi.GetRootBudgets(request));
            }
            catch (AuthenticationException)
            {
                return Forbid();
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateBudget(ApiRequest request)
        {
            try
            {
                return new JsonResult(await _budgetApi.UpdateBudget(request));
            }
            catch (AuthenticationException)
            {
                return Forbid();
            }
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetBudget(ApiRequest request)
        {
            try
            {
                return new JsonResult(await _budgetApi.GetBudget(request));
            }
            catch (AuthenticationException)
            {
                return Forbid();
            }
        }

        [HttpPost("tree")]
        public async Task<IActionResult> FetchBudgetTree(ApiRequest request)
        {
            try
            {
                return new JsonResult(await _budgetApi.FetchBudgetTree(request));
            }
            catch (AuthenticationException)
            {
                return Forbid();
            }
        }
    }
}
