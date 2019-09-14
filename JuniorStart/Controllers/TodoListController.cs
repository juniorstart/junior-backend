using System.Collections.Generic;
using JuniorStart.DTO;
using JuniorStart.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JuniorStart.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/todolists")]
    [Authorize]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListService _todoListService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TodoListController(ITodoListService todoListService,IHttpContextAccessor httpContextAccessor)
        {
            _todoListService = todoListService;
            _httpContextAccessor = httpContextAccessor;
        }
        
        /// <summary>
        /// Get task with given id
        /// </summary>
        /// <param name="id">Task id</param>
        /// <returns></returns>
        /// <response code="200">Returns task</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If unexpected error appear</response>
        [HttpGet("task/{id}",Name = "GetTaskById")]
        [ProducesResponseType(typeof(TaskDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult GetTask(int id)
        {
            return Ok(_todoListService.GetTaskById(id));
        }
        
        /// <summary>
        /// Get todolist with given id
        /// </summary>
        /// <param name="id">TodoList id</param>
        /// <returns></returns>
        /// <response code="200">Returns task</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If unexpected error appear</response>
        [HttpGet("todolist/{id}",Name = "GetTodoListById")]
        [ProducesResponseType(typeof(TodoListDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult GetTodoList(int id)
        {
            return Ok(_todoListService.GetTodoListById(id));
        }
        
        /// <summary>
        /// Get all Todolists for user
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns todolists</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If unexpected error appear</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<TodoListDto>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult GetAllTodoLists()
        {
            return Ok(_todoListService.GetTodoListsForUser(int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name)));
        }

        /// <summary>
        /// Add new TodoList
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /todolist
        ///     {
        ///        "name": "Test",
        ///     }
        /// </remarks>
        /// <param name="requestModel">request object</param>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is not valid.</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If unexpected error appear</response>
        /// <returns></returns>
        [HttpPost("todolist")]
        [ProducesResponseType(typeof(TodoListDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult CreateTodoList(TodoListDto requestModel)
        {
            requestModel.OwnerId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            _ = _todoListService.CreateTodoList(requestModel);
            return CreatedAtRoute("GetTodoListById", new {id = requestModel.Id}, requestModel);
        }
        
        /// <summary>
        /// Add new task
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /task
        ///     {
        ///        "description": "Test",
        ///        "todoListId": 1,
        ///     }
        /// </remarks>
        /// <param name="requestModel">request object</param>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is not valid.</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If unexpected error appear</response>
        /// <returns></returns>
        [HttpPost("task")]
        [ProducesResponseType(typeof(TaskDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult CreateTask(TaskDto requestModel)
        {
            _ = _todoListService.CreateTask(requestModel);
            return CreatedAtRoute("GetTaskById", new {id = requestModel.Id}, requestModel);
        }

        /// <summary>
        /// Update recruitment info
        /// </summary>
        /// <param name="requestModel">Model to update</param>
        /// <param name="taskId">Id of model</param>
        /// <response code="200">Returns updated object</response>
        /// <response code="400">If the item is not valid.</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If unexpected error appear</response>
        /// <returns></returns>
        [HttpPut("{taskId}", Name = "updateTask")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult Put([FromBody] TaskDto requestModel, int taskId)
        {
            _ = _todoListService.UpdateTask(taskId, requestModel);
            return StatusCode(200, requestModel);
        }
        
        /// <summary>
        /// Archives task
        /// </summary>
        /// <param name="id">Task id</param>
        /// <returns></returns>
        /// <response code="204">Returns if successfully archived object</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If unexpected error appear</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [HttpDelete("task/{id}", Name = "DeleteTask")]
        public IActionResult DeleteTask(int id)
        {
            _todoListService.ArchiveTask(id);
            return NoContent();
        }
        
        /// <summary>
        /// Archives TodoList
        /// </summary>
        /// <param name="id">TodoList id</param>
        /// <returns></returns>
        /// <response code="204">Returns if successfully archived object</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If unexpected error appear</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [HttpDelete("{id}", Name = "DeleteTodoList")]
        public IActionResult DeleteTodoList(int id)
        {
            _todoListService.ArchiveTodoList(id);
            return NoContent();
        }
    }
}