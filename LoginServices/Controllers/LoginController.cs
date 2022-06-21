using CommonServices.Model;
using LoginServices.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class LoginController : ControllerBase
    {
            private IUserRepository _userService;

            public LoginController(IUserRepository userService)
            {
                _userService = userService;
            }

            [HttpPost("authenticate")]
            public IActionResult Authenticate(AuthenticateRequest model)
            {
                var response = _userService.Authenticate(model);

                if (response == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                return Ok(response);
            }

            [Authorize(Roles = "admin")]
            [HttpGet]
            [Route("GetResult")]
            public IActionResult GetAll()
            {
                var users = _userService.GetAll();
                return Ok(users);
            }
        [HttpPost]
        [Route("Register")]
        public void Register(User u)
        {
             _userService.Add(u);
        }

    }
}
