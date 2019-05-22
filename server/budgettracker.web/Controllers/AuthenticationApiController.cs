using budgettracker.business.Api;
using budgettracker.business.Api.Contracts.Responses;
using budgettracker.business.Api.Contracts.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace budgettracker.web.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationApiController : ControllerBase
    {
        [HttpPost]
        public ApiResponse Invoke(ApiRequest requestMessage)
        {
            IServiceProvider serviceProvider = HttpContext.RequestServices;
            return AuthenticationApi.Invoke(requestMessage, serviceProvider);
        }
    }
}