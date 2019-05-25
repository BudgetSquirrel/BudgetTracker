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
        [HttpGet]
        public ApiResponse Get() {
            Rfc2898Encryptor encryptor = new Rfc2898Encryptor();
            byte[] salt = new byte[] {
                0x53, 0x71, 0x75, 0x69, 0x72, 0x72, 0x65, 0x6c, 0x20, 0x50, 0x6f, 0x77, 0x65, 0x72
            };
            string encryptionKey = "Acorns";
            string hi = "Hello world!";
            string hiEncrypted = encryptor.Encrypt(hi, encryptionKey, salt);
            string hiDecrypted = encryptor.Decrypt(hiEncrypted, encryptionKey, salt);

            Console.WriteLine(hiEncrypted);
            Console.WriteLine(hiDecrypted);
            return new ApiResponse(hiEncrypted);
        }

        [HttpPost("register")]
        public ApiResponse Register(UserRequestApiContract userValues)
        {
            IServiceProvider serviceProvider = HttpContext.RequestServices;
            return AuthenticationApi.Register(userValues, serviceProvider);
        }
    }
}