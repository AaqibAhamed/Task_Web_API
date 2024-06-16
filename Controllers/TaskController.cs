using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/tasks")]
public class TaskController : ControllerBase
{
    //private readonly TaskDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<TaskController> _logger;

    public TaskController(IMapper mapper, ITaskRepository taskRepository, ILogger<TaskController> logger)
    {
        //_context = context;
        _mapper  = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        _logger = logger ??  throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Task>>> GetAllTasks()
    {
        var tasks = await _taskRepository.GetAllTasksAsync();

        if(tasks == null)
        {
            return NotFound();
        }
        
        return Ok(_mapper.Map<IEnumerable<TaskDto>>(tasks));
    }

    [HttpGet("{id}", Name ="GetTask")]
    public async Task<ActionResult<Task>> GetTask(int id)
    {
        /* if(!await _taskRepository.TaskExistsAsync(id))
        {
            return NotFound();
        } */
        var task = await _taskRepository.GetTaskAsync(id);

        if (task == null)
        {
            return NotFound(new ProblemDetails
            {
                Status = 404,
                Title = "Task not found",
                Detail = "The specified task was not found in the database."
            });
        }

        return Ok(_mapper.Map<TaskDto>(task));
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> CreateTask(TaskCreateDto task)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var taskToCreate = _mapper.Map<Task>(task);

        _taskRepository.AddTask(taskToCreate);

        await _taskRepository.SaveChangesAsync();

        var createdTaskToReturn = _mapper.Map<TaskDto>(taskToCreate);

        return CreatedAtAction(nameof(GetTask), new { id = createdTaskToReturn.Id }, createdTaskToReturn);
    }

    [HttpPut("{id}", Name ="UpdateTask")]
    public async Task<ActionResult> UpdateTask(int id, TaskUpdateDto taskToUpdate)
    {
         if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var taskEntity = await _taskRepository.GetTaskAsync(id);

        if (taskEntity == null)
        {
            return NotFound();
        }

        _mapper.Map(taskToUpdate,taskEntity);

        await _taskRepository.SaveChangesAsync();

        return Ok(taskEntity); // return NoContent(); not looks nice

    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> PartiallyUpdateTask(int id, JsonPatchDocument<TaskUpdateDto> patchDocument)
    {
        var taskEntity = await _taskRepository.GetTaskAsync(id);

        if(taskEntity == null)
        {
            return NotFound();
        }

        var taskToPatch = _mapper.Map<TaskUpdateDto>(taskEntity);

        patchDocument.ApplyTo(taskToPatch, ModelState);

        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if(!TryValidateModel(taskToPatch))
        {
            return BadRequest(ModelState);
        }

        _mapper.Map(taskToPatch,taskEntity);

        await _taskRepository.SaveChangesAsync();

        return Ok(taskEntity); // NoContent();
    }

     [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTask(int id)
    {
      /*   if(! await _taskRepository.TaskExistsAsync(id))
        {
            return NotFound();
        } */
        var taskEntity = await _taskRepository.GetTaskAsync(id);

        if(taskEntity == null)
        {
            return NotFound();
        }

        _taskRepository.DeleteTask(taskEntity);

        await _taskRepository.SaveChangesAsync();

        return NoContent();
    }


}
