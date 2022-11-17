using API.Entities.Utils;
using API.Infra;
using API.Services;
using Microsoft.Extensions.Options;

namespace API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;        

        public JwtMiddleware(RequestDelegate next, IOptions<TokenManagement> tokenManagement)
        {
            _next = next;            
        }

        public async Task Invoke(HttpContext context, UserService userService, TokenService tokenService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = tokenService.ValidateJwtToken(token);
            if (userId != null)
            {                
                context.Items["User"] = userService.GetByUsername(userId);
            }

            await _next(context);
        }
    }
}
