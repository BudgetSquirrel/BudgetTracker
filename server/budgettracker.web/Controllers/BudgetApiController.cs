using budgettracker.business.Api.Contracts.Responses;
using budgettracker.business.Api.Contracts.Requests;
using budgettracker.business.Api.Interfaces;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using System.Security.Authentication;
using System;
using budgettracker.business.Api.Contracts.BudgetApi;

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
    }
}
