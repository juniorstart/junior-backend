using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JuniorStart.Models;
using JuniorStart.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JuniorStart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationContext context;

        public ValuesController(ApplicationContext _context)
        {
            context = _context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<User> Get()
        {
            return context.Users; 
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return NoContent();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}