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
        [HttpGet("getAllTasks")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetAllTasks()
        {
            var response = await _toDoService.GetAllTasksAsync();

            return Ok(response);
        }

        /// <summary>
        /// Get a Task by Id
        /// </summary>
        /// <param name="taskId">The Id of the Task to get </param>
        /// <returns> A Task with Details</returns>
        [HttpGet("getTask")]
        public async Task<ActionResult<ToDoItem>> GetTask(Guid taskId)
        {
            var task = await _toDoService.GetTaskByIdAsync(taskId);

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
        [HttpPost("createTask")]
        public async Task<IActionResult> CreateTask(ToDoItemCreateDto task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var responseData = await _toDoService.CreateTaskAsync(task);

            if (!responseData.Success)
            {
                return NotFound(responseData);
            }

            return Ok(responseData);
        }

        /// <summary>
        /// Update a Task
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="taskToUpdate"></param>
        /// <returns></returns>
        [HttpPut("updateTask")]
        public async Task<IActionResult> UpdateTask(Guid taskId, ToDoItemUpdateDto taskToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedTaskResponse = await _toDoService.EditTaskAsync(taskId, taskToUpdate);

            if (!updatedTaskResponse.Success)
            {
                return BadRequest(updatedTaskResponse);  //NotFound  will be added in the future
            }

            return Ok(updatedTaskResponse);

        }

        /// <summary>
        /// Partially Update a Task with patchDocument
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        [HttpPatch("partiallyUpdateTask")]
        public async Task<IActionResult> PartiallyUpdateTask(Guid taskId, JsonPatchDocument<ToDoItemUpdateDto?> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest(new ResponseDto
                {
                    Success = false,
                    Message = "Patch document is empty",
                    Data = null
                });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patchResponse = await _toDoService.PatchTaskAsync(taskId, patchDocument);


            if (!patchResponse.Success)
            {
                return NotFound(patchResponse);
            }

            return Ok(patchResponse);
        }

        /// <summary>
        /// Delete Task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpDelete("deleteTask")]
        public async Task<IActionResult> DeleteTask(Guid taskId)
        {
            var response = await _toDoService.DeleteTaskAsync(taskId);

            if (!response.Success)
                return NotFound(response); // Returns 404 if task not found

            return Ok(response); // Returns 200 with success message

        }
    }
}