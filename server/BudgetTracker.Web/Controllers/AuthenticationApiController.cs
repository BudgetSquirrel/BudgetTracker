using BudgetTracker.Business.Api;
using BudgetTracker.Business.Api.Contracts.AuthenticationApi;
using BudgetTracker.Business.Api.Contracts.Responses;
using BudgetTracker.Business.Api.Contracts.Requests;
using GateKeeper.Cryptogrophy;
using GateKeeper.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetTracker.Web.Controllers
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
        AuthenticationApi _authApi;

        public AuthenticationApiController(AuthenticationApi authApi)
        {
            _authApi = authApi;
        }

        [HttpPost("register")]
        public async Task<ApiResponse> Register(ApiRequest request)
        {
            return await _authApi.Register(request);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(ApiRequest request)
        {
            try
            {
                return new JsonResult(await _authApi.AuthenticateUser(request));
            }
            catch(AuthenticationException)
            {
                return Forbid();
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUser(ApiRequest request)
        {
            try
            {
                return new JsonResult(await _authApi.DeleteUser(request));
            }
            catch(AuthenticationException)
            {
                return Forbid();
            }
        }
    }
}
