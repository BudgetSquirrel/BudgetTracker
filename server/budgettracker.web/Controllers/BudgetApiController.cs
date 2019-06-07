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
            if(!request.Arguments<CreateBudgetRequestContract>().IsValid())
            {
                return BadRequest();
            }

            try
            {
                return Ok(await _budgetApi.CreateBudget(request));    
            }
            catch (Exception ex) when (ex.InnerException is AuthenticationException)
            {
                if (ex is AuthenticationException) 
                {
                    return Forbid();
                }
                // We should consider logging the exception here. 
                return StatusCode(500);
            }            
        }
    }
}