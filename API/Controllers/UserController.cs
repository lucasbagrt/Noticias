using API.Entities;
using API.Entities.Utils;
using API.Entities.ViewModels;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;

        public UserController(ILogger<UserController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        [HttpGet("{page}/{qtd}")]
        public ActionResult<Result<UserViewModel>> Get(int page, int qtd) => _userService.Get(page, qtd);

        [HttpGet("{id:length(24)}", Name = "GetUsers")]
        public ActionResult<UserViewModel> Get(string id)
        {
            var user = _userService.Get(id);

            if (user is null)
                return NotFound();

            return user;
        }

        [HttpPost]
        public ActionResult<UserViewModel> Create(UserViewModel user)
        {
            var result = _userService.Create(user);

            return CreatedAtRoute("GetUsers", new { id = result.Id.ToString() }, result);
        }


        [HttpPut("{id:length(24)}")]
        public ActionResult<UserViewModel> Update(string id, UserViewModel userIn)
        {
            var user = _userService.Get(id);

            if (user is null)
                return NotFound();

            _userService.Update(id, userIn);

            return CreatedAtRoute("GetUsers", new { id = id }, userIn);

        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var user = _userService.Get(id);

            if (user is null)
                return NotFound();

            _userService.Remove(user.Id);

            return Ok("Noticia deletada com sucesso!");
        }
    }
}
