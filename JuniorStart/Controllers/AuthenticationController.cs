using System;
using JuniorStart.Entities;
using JuniorStart.Filters;
using JuniorStart.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuniorStart.Controllers
{
    [ApiController]
    [Route("/")]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _authenticationService;
        private IUserService _userService;

        public AuthenticationController(IAuthenticationService authenticationService,IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }
        
        [ModelValidation]
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            try
            {
                string authenticated = _authenticationService.Authenticate(userParam.Login, userParam.Password);
                return Ok(authenticated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ModelValidation]
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] User userParam)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(_userService.Create(userParam));
        }
        
    }
}