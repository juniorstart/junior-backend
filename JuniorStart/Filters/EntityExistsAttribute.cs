using System.Linq;
using JuniorStart.Entities;
using JuniorStart.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JuniorStart.Filters
{
    public class EntityExistsAttribute<T> : IActionFilter where T : class, IEntity
    {
        private readonly ApplicationContext _context;

        public EntityExistsAttribute(ApplicationContext context)
        {
            _context = context;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            int id;
            if (context.ActionArguments.ContainsKey("id"))
            {
                id = (int) context.ActionArguments["id"];
            }
            else
            {
                context.Result = new BadRequestObjectResult("No id parameter");
                return;
            }

            var entity = _context.Set<T>().SingleOrDefault(x => x.Id.Equals(id));
            if (entity == null)
            {
                context.Result = new NotFoundObjectResult("Entity not found");
            }
            else
            {
                context.HttpContext.Items.Add("entity", entity);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}