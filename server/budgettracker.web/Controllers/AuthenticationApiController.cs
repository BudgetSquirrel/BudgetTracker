using budgettracker.business.Api;
using budgettracker.business.Api.Contracts.AuthenticationApi;
using budgettracker.business.Api.Contracts.Responses;
using budgettracker.business.Api.Contracts.Requests;
using GateKeeper.Cryptogrophy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace budgettracker.web.Controllers
{
    /// <summary>
    /// <p>
    /// The controller for the authentication API. All requests dealing with
    /// user accounts and authentication should be routed through here.
    /// </p>
    /// <p>
    /// This simply passes the request on to the code based API for authentication.
    /// </p>
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationApiController : ControllerBase
    {
        [HttpPost("register")]
        public ApiResponse Register(ApiRequest request)
        {
            IServiceProvider serviceProvider = HttpContext.RequestServices;
            return AuthenticationApi.Register(request, serviceProvider);
        }

        [HttpPost("authenticate")]
        public ApiResponse Authenticate(ApiRequest request)
        {
            IServiceProvider serviceProvider = HttpContext.RequestServices;
            return AuthenticationApi.Authenticate(request, serviceProvider);
        }
    }
}