using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace BudgetTracker.BudgetSquirrel.WebApi.Authorization
{
    public class BasicAuthenticationHandler : IAuthenticationHandler
    {
        private HttpContext _context;

        public async Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            await Task.Run(() => {
                _context = context;
            });
        }

        public async Task<AuthenticateResult> AuthenticateAsync()
            => await Task.FromResult(AuthenticateResult.NoResult());

        public async Task ChallengeAsync(AuthenticationProperties properties)
        {
            await Task.Run(() => {
                _context.Response.StatusCode = 401;
            });
        }

        public async Task ForbidAsync(AuthenticationProperties properties)
        {
            await Task.Run(() => {
                _context.Response.StatusCode = 403;
            });
        }
    }
}
