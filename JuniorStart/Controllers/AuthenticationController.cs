using JuniorStart.DTO;
using JuniorStart.Services.Interfaces;
using JuniorStart.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace JuniorStart.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly ILogger<AuthenticationController> _logger;
        
        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService, ILogger<AuthenticationController> logger)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="userParam">Login and password</param>
        /// <returns>JWT token</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /authenticate
        ///     {
        ///         "login": "johnWick",
        ///         "password": "J0hnw!ck"
        ///     }
        /// </remarks>
        /// <response code="200">Returns the JWT Token</response>
        /// <response code="500">If unexpected error appear</response>
        [ProducesResponseType(typeof(SecurityToken), 200)]
        [ProducesResponseType(500)]
        [HttpPost("/Login")]
        public IActionResult Authenticate([FromBody] LoginRequest userParam)
        {
            string authenticated = _authenticationService.Authenticate(userParam.Login, userParam.Password);
            _logger.LogInformation(string.Format("User {0} has been authenticated", userParam.Login));
            return Ok(authenticated);
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="userParam">user information</param>
        /// <returns>Returns register status</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /register
        ///     {
        ///         "firstName": "Jonathan",
        ///         "lastName": "Wick",
        ///         "email": "johnwick@example.com",
        ///         "login": "johnWick",
        ///         "password": "J0hnw!ck"
        ///     }
        /// </remarks>
        /// <response code="200">Returns register status</response>
        /// <response code="500">If unexpected error appear</response>
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(500)]
        [HttpPost("/Register")]
        public IActionResult Register([FromBody] UserViewModel userParam)
        {
            _logger.LogInformation(string.Format("User with login {0} has been created"), userParam.User.Login);
            return Ok(_userService.Create(userParam.User));
        }
    }
}