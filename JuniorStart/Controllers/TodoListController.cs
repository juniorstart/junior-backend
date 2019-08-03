using System.Collections.Generic;
using JuniorStart.DTO;
using JuniorStart.Entities;
using JuniorStart.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        public TodoListController(ITodoListService todoListService)
        {
            _todoListService = todoListService;
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
        /// <param name="id">Owner id</param>
        /// <returns></returns>
        /// <response code="200">Returns todolists</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If unexpected error appear</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<TodoListDto>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult GetAllTodoLists(int ownerId)
        {
            return Ok(_todoListService.GetTodoListsForUser(ownerId));
        }

        /// <summary>
        /// Add new TodoList
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /todolist/create
        ///     {
        ///        "name": "Test",
        ///        "ownerId": 1,
        ///     }
        /// </remarks>
        /// <param name="requestModel">request object</param>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is not valid.</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If unexpected error appear</response>
        /// <returns></returns>
        [HttpPost("todolist/create")]
        [ProducesResponseType(typeof(TodoListDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult CreateTodoList(TodoListDto requestModel)
        {
            _ = _todoListService.CreateTodoList(requestModel);
            return CreatedAtRoute("GetTodoListById", new {id = requestModel.Id}, requestModel);
        }
        
        /// <summary>
        /// Add new TodoList
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /task/create
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
        [HttpPost("task/create")]
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
        [HttpPut("{id}", Name = "updateInformation")]
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
        [HttpDelete("{id}", Name = "DeleteTask")]
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