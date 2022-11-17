using API.Entities.ViewModels;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly TokenService _tokenService;

        public AuthController(ILogger<AuthController> logger, TokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] UserViewModel request)
        {
            string token;
            if (_tokenService.IsAuthenticated(request, out token))
            {
                return Ok(new { token = token });
            }
            else
            {
                return BadRequest("Usuario ou senha invalido(s)!");
            }
        }
    }
}
