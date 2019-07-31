using JuniorStart.DTO;
using JuniorStart.Services.Interfaces;
using JuniorStart.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JuniorStart.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
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
        [HttpPost]
        public IActionResult Authenticate([FromBody] LoginRequest userParam)
        {
            string authenticated = _authenticationService.Authenticate(userParam.Login, userParam.Password);
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
        [HttpPost]
        public IActionResult Register([FromBody] UserViewModel userParam)
        {
            return Ok(_userService.Create(userParam.User));
        }
    }
}