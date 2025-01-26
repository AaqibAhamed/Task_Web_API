using AutoMapper;
using Task_Web_API.Entities;
using Task_Web_API.Middlewares;
using Task_Web_API.Models;
using Task_Web_API.Repositories;

namespace Task_Web_API.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ToDoService> _logger;

        public ToDoService(IToDoRepository toDoRepository, IMapper mapper, ILogger<ToDoService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _toDoRepository = toDoRepository ?? throw new ArgumentNullException(nameof(toDoRepository));
        }

        public async Task<ResponseDto> GetAllTasksAsync()
        {
            try
            {
                var tasksList = await _toDoRepository.GetAllTasksAsync();

                if (tasksList == null)
                {
                    return new ResponseDto
                    {
                        Success = false,
                        Message = "Failed to load Task List. Please try again later."
                    };
                }

                return new ResponseDto
                {
                    Success = true,
                    Message = "Tasks retrieved successfully",
                    Data = _mapper.Map<IEnumerable<ToDoItemDto>>(tasksList)
                };
            }
            catch (Exception ex)
            {
                // Log the exception (ex) if necessary
                _logger.LogError(ex, "Error occurred while retrieving tasks");

                return new ResponseDto
                {
                    Success = false,
                    Message = "An error occurred while processing your request."
                };
            }
        }

        public async Task<ToDoItemDto> GetTaskByIdAsync(Guid taskId)
        {
            var task = await _toDoRepository.FindTaskByIdAsync(taskId) ?? throw new TaskNotFoundException(taskId);

            return _mapper.Map<ToDoItemDto>(task);
        }

        public async Task<ResponseDto> CreateTaskAsync(ToDoItemCreateDto toDoItemCreateDto)
        {
            ToDoItem toDoItemEntity = _mapper.Map<ToDoItem>(toDoItemCreateDto);

            try
            {
                Guid createdTaskId = await _toDoRepository.AddTaskAsync(toDoItemEntity);

                if (createdTaskId == Guid.Empty)
                {
                    return new ResponseDto
                    {
                        Success = false,
                        Message = "Failed to create Task. Please try again later."
                    };
                }

                return new ResponseDto
                {
                    Success = true,
                    Message = "Task created successfully",
                    Data = createdTaskId //toDoItemEntity.Id
                };
            }
            catch (Exception ex)
            {
                // Log the exception (ex) if necessary
                _logger.LogError(ex, "Error occurred while creating a Task");

                return new ResponseDto
                {
                    Success = false,
                    Message = "An error occurred while processing your create request."
                };
            }
        }

        public async Task<ResponseDto> EditTaskAsync(Guid taskId, ToDoItemUpdateDto toDoItemUpdateDto)
        {
            ToDoItem taskToUpdate = _mapper.Map<ToDoItem>(toDoItemUpdateDto);

            try
            {
                var updatedTask = await _toDoRepository.UpdateTaskAsync(taskId, taskToUpdate) ?? throw new TaskNotFoundException(taskId);

                return new ResponseDto
                {
                    Success = true,
                    Message = "Task updated successfully",
                    Data = _mapper.Map<ToDoItemDto>(updatedTask)  //updatedTask
                };

            }
            catch (Exception ex)
            {
                // Log the exception (ex) if necessary
                _logger.LogError(ex, "Error occurred while updating a Task");

                return new ResponseDto
                {
                    Success = false,
                    Message = "An error occurred while processing your update request."
                };

            }
        }

        public async Task<ResponseDto> DeleteTaskAsync(Guid id)
        {
            bool isDeleted = await _toDoRepository.RemoveTaskAsync(id);

            if (!isDeleted)
            {
                return new ResponseDto
                {
                    Success = false,
                    Message = "Task not found or already deleted."
                };
            }

            return new ResponseDto
            {
                Success = true,
                Message = "Task deleted successfully."
            };
        }

        public async Task<IEnumerable<ToDoItem>> GetAllCompletedTasksAsync(bool? IsCompleted)
        {
            return await _toDoRepository.GetAllCompletedTasksAsync(IsCompleted);
        }

        public async Task<IEnumerable<ToDoItem>> GetAllPendingTasksAsync(bool? IsCompleted)
        {
            return await _toDoRepository.GetAllPendingTasksAsync(IsCompleted);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _toDoRepository.SaveChangesAsync();
        }

        public Task<bool> TaskExistsAsync(Guid taskId)
        {
            return _toDoRepository.TaskExistsAsync(taskId);
        }

        // var taskEntity = await _toDoService.GetTaskByIdAsync(id);

        //     if (taskEntity == null)
        //     {
        //         return NotFound();
        //     }

        //     var taskToPatch = _mapper.Map<ToDoItemUpdateDto>(taskEntity);

        //     patchDocument.ApplyTo(taskToPatch, ModelState);

        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     if (!TryValidateModel(taskToPatch))
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     _mapper.Map(taskToPatch, taskEntity);

        //     await _toDoService.SaveChangesAsync();

        //     return Ok(taskEntity); 



        // public async Task<ResponseDto> CreateTaskAsync(ToDoItemCreateDto toDoItemCreateDto)
        // {
        //     ToDoItem toDoItemEntity = _mapper.Map<ToDoItem>(toDoItemCreateDto);

        //     Guid createdToDoItemId = await _toDoRepository.AddTaskAsync(toDoItemEntity);

        //     if (createdToDoItemId == Guid.Empty)
        //     {
        //         return new ResponseDto
        //         {
        //             Success = false,
        //             Message = "Task not created",
        //             Data = null
        //         };
        //     }

        //     else
        //     {
        //         return new ResponseDto
        //         {
        //             Success = true,
        //             Message = "Task created successfully",
        //             Data = createdToDoItemId
        //         };
        //     }
        // }




    }

}