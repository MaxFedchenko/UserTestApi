using Microsoft.AspNetCore.Mvc;
using UserTestApi.Business.Services;
using UserTestApi.DTOs;

namespace UserTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Login(LoginDTO dto) 
        {
            if (await _userService.Exists(dto.UserName))
                return Ok();

            await _userService.CreateNewOne(dto.UserName);
            return StatusCode(201);
        }
    }
}
