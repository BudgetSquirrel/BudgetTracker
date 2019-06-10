using budgettracker.business.Api;
using budgettracker.business.Api.Contracts.AuthenticationApi;
using budgettracker.business.Api.Contracts.Responses;
using budgettracker.business.Api.Contracts.Requests;
using GateKeeper.Cryptogrophy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        AuthenticationApi _authApi;

        public AuthenticationApiController(AuthenticationApi authApi)
        {
            _authApi = authApi;
        }

        [HttpPost("register")]
        public ApiResponse Register(ApiRequest request)
        {
            return _authApi.Register(request);
        }

        [HttpPost("authenticate")]
        public async Task<ApiResponse> Authenticate(ApiRequest request)
        {
            return await _authApi.AuthenticateUser(request);
        }

        [HttpPost("delete")]
        public async Task<ApiResponse> DeleteUser(ApiRequest request)
        {
            return await _authApi.DeleteUser(request);
        }
    }
}
