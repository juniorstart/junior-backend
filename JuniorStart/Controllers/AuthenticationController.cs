using System;
using JuniorStart.DTO;
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

        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [ModelValidation]
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginModel userParam)
        {
            string authenticated = _authenticationService.Authenticate(userParam.Login, userParam.Password);
            return Ok(authenticated);
        }

        [ModelValidation]
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] User userParam)
        {
            return Ok(_userService.Create(userParam));
        }
    }
}