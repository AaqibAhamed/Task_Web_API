using Microsoft.AspNetCore.JsonPatch;
using Task_Web_API.Entities;
using Task_Web_API.Models;

namespace Task_Web_API.Services
{
    public interface IToDoService
    {
        Task<ResponseDto> GetAllTasksAsync();

        Task<ToDoItemDto> GetTaskByIdAsync(Guid taskId);

        Task<ResponseDto> CreateTaskAsync(ToDoItemCreateDto toDoItemCreateDto);

        Task<ResponseDto> EditTaskAsync(Guid taskId, ToDoItemUpdateDto toDoItemUpdateDto);

        Task<ResponseDto> PatchTaskAsync(Guid taskId, JsonPatchDocument<ToDoItemUpdateDto?> patchDocument);

        Task<ResponseDto> DeleteTaskAsync(Guid id);

        Task<IEnumerable<ToDoItem>> GetAllCompletedTasksAsync(bool? IsCompleted);

        Task<IEnumerable<ToDoItem>> GetAllPendingTasksAsync(bool? IsCompleted);

        Task<bool> TaskExistsAsync(Guid taskId);

        Task<bool> SaveChangesAsync();

       // Task<ResponseDto> PatchTaskAsyncMapper(Guid id, JsonPatchDocument<ToDoItemUpdateDto> patchDocument);

    }
}