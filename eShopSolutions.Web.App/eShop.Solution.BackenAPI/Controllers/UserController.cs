using eShop.Solution.ViewModels.System;
using eShopSolutions.Application.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Solution.BackenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("authencation")]
        [AllowAnonymous]
        public async Task<IActionResult> Authencation([FromForm] LoginResquest resquest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultToken = await userService.Authencation(resquest);
            if (string.IsNullOrEmpty(resultToken))
            {
                return BadRequest("Username or password incorrect");
            }
            return Ok(new { token = resultToken });
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterRequest resquest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await userService.Register(resquest);
            if (!result)
            {
                return BadRequest("Register is unsucessful");
            }
            return Ok("Register sucscess");
        }

    }
}
