using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Task_Web_API.Entities;
using Task_Web_API.Models;
using Task_Web_API.Repositories;

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
        private readonly IMapper _mapper;
        private readonly IToDoRepository _toDoRepository;
        private readonly ILogger<TaskController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="toDoRepository"></param>
        /// <param name="logger"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public TaskController(IMapper mapper, IToDoRepository toDoRepository, ILogger<TaskController> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _toDoRepository = toDoRepository ?? throw new ArgumentNullException(nameof(toDoRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get All Tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetAllTasks()
        {
            var tasks = await _toDoRepository.GetAllTasksAsync();

            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ToDoItemDto>>(tasks));
        }

        /// <summary>
        /// Get a Task by Id
        /// </summary>
        /// <param name="id">The Id of the Task to get </param>
        /// <returns> A Task with Details</returns>
        [HttpGet("{id}", Name = "GetTask")]
        public async Task<ActionResult<ToDoItem>> GetTask(int id)
        {

            var task = await _toDoRepository.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound(new ProblemDetails
                {
                    Status = 404,
                    Title = "Task not found",
                    Detail = "The specified task was not found in the database."
                });
            }

            return Ok(_mapper.Map<ToDoItemDto>(task));
        }

        /// <summary>
        /// Create a Task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ToDoItemDto>> CreateTask(ToDoItemCreateDto task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var taskToCreate = _mapper.Map<ToDoItem>(task);

            _toDoRepository.AddTask(taskToCreate);

            await _toDoRepository.SaveChangesAsync();

            var createdTaskToReturn = _mapper.Map<ToDoItemDto>(taskToCreate);

            return CreatedAtAction(nameof(GetTask), new { id = createdTaskToReturn.Id }, createdTaskToReturn);
        }

        /// <summary>
        /// Update a Task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="taskToUpdate"></param>
        /// <returns></returns>
        [HttpPut("{id}", Name = "UpdateTask")]
        public async Task<ActionResult> UpdateTask(int id, ToDoItemUpdateDto taskToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var taskEntity = await _toDoRepository.GetTaskByIdAsync(id);

            if (taskEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(taskToUpdate, taskEntity);

            await _toDoRepository.SaveChangesAsync();

            return Ok(taskEntity); // return NoContent(); not looks nice

        }

        /// <summary>
        /// Partially Update a Task with patchDocument
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartiallyUpdateTask(int id, JsonPatchDocument<ToDoItemUpdateDto> patchDocument)
        {
            var taskEntity = await _toDoRepository.GetTaskByIdAsync(id);

            if (taskEntity == null)
            {
                return NotFound();
            }

            var taskToPatch = _mapper.Map<ToDoItemUpdateDto>(taskEntity);

            patchDocument.ApplyTo(taskToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(taskToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(taskToPatch, taskEntity);

            await _toDoRepository.SaveChangesAsync();

            return Ok(taskEntity); // NoContent();
        }

        /// <summary>
        /// Delete Task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            /*   if(! await _taskRepository.TaskExistsAsync(id))
              {
                  return NotFound();
              } */
            var taskEntity = await _toDoRepository.GetTaskByIdAsync(id);

            if (taskEntity == null)
            {
                return NotFound();
            }

            _toDoRepository.DeleteTask(taskEntity);

            await _toDoRepository.SaveChangesAsync();

            return NoContent();
        }


    }
}