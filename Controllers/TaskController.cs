using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Task_Web_API.Entities;
using Task_Web_API.Models;
using Task_Web_API.Services;

namespace Task_Web_API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/tasks")]
    [Authorize]
    //[Authorize(policy: "MustBeWiki")]  // - IF you uncomment it use userName as "wiki"
    [ApiVersion(1)]
    [ApiVersion(2)]

    public class TaskController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toDoService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public TaskController(IToDoService toDoService)
        {
            _toDoService = toDoService ?? throw new ArgumentNullException(nameof(toDoService));
        }

        /// <summary>
        /// Get All Tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetAllTasks()
        {
            var response = await _toDoService.GetAllTasksAsync();

            return Ok(response);
        }

        /// <summary>
        /// Get a Task by Id
        /// </summary>
        /// <param name="id">The Id of the Task to get </param>
        /// <returns> A Task with Details</returns>
        [HttpGet("{id}", Name = "GetTask")]
        public async Task<ActionResult<ToDoItem>> GetTask(Guid id)
        {
            var task = await _toDoService.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound(new ProblemDetails
                {
                    Status = 404,
                    Title = "Task not found",
                    Detail = "The specified task was not found in the database."
                });
            }

            return Ok(new
            {
                StatusCode = 200,
                Message = "Task retrieved successfully",
                Data = task

            });
        }

        /// <summary>
        /// Create a Task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> CreateTask(ToDoItemCreateDto task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var responseData = await _toDoService.CreateTaskAsync(task);

            if (!responseData.Success)
            {
                return BadRequest(responseData);
            }

            return Ok(responseData);
        }

        /// <summary>
        /// Update a Task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="taskToUpdate"></param>
        /// <returns></returns>
        [HttpPut("{id}", Name = "UpdateTask")]
        public async Task<IActionResult> UpdateTask(Guid id, ToDoItemUpdateDto taskToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedTask = await _toDoService.EditTaskAsync(id, taskToUpdate);

            if (!updatedTask.Success)
            {
                return BadRequest(updatedTask);
            }

            return Ok(updatedTask);

        }

        /// <summary>
        /// Partially Update a Task with patchDocument
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public ActionResult PartiallyUpdateTask(Guid id, JsonPatchDocument<ToDoItemUpdateDto> patchDocument)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete Task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var response = await _toDoService.DeleteTaskAsync(id);

            if (!response.Success)
                return NotFound(response); // Returns 404 if task not found

            return Ok(response); // Returns 200 with success message

        }
    }
}