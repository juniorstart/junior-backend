using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JuniorStart.Filters;
using JuniorStart.Entities;
using JuniorStart.DTO;
using JuniorStart.Repository;
using Microsoft.AspNetCore.Mvc;

namespace JuniorStart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationContext context;

        public ValuesController(ApplicationContext _context)
        {
            context = _context;
        }

        /// <summary>
        /// Get Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IList<UserViewModel> Get()
        {
            return context.Users.ToList().ConvertAll(x => new UserViewModel()
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Login = x.Login,
                Password = x.Password
            });
        }

        [ServiceFilter(typeof(EntityExistsAttribute<User>))]
        [HttpGet("{id}")]
        public ActionResult<string> GetById(int id)
        {
            var user = HttpContext.Items["entity"] as User;
            return Ok(user);
        }


        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="userm"></param>
        /// <returns></returns>
        /// <response code="204">If successfully created user</response>
        [ServiceFilter(typeof(ModelValidation))]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserViewModel userm)
        {
            var user = new User()
            {
                Email = userm.Email,
                Password = userm.Password,
                Login = userm.Login,
                FirstName = userm.Password,
                LastName = userm.LastName,
                IsActive = true
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id = user.Id}, user);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}