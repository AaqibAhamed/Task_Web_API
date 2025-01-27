using Microsoft.AspNetCore.JsonPatch;
using Task_Web_API.Entities;

namespace Task_Web_API.Repositories
{
    public interface IToDoRepository
    {
        Task<IEnumerable<ToDoItem>> GetAllTasksAsync();

        Task<ToDoItem?> FindTaskByIdAsync(Guid taskId);

        Task<Guid> AddTaskAsync(ToDoItem toDoItem);

        Task<ToDoItem?> UpdateTaskAsync(Guid taskId, ToDoItem toDoItem);

        Task<ToDoItem?> ApplyPatchTaskAsync(Guid taskId, JsonPatchDocument<ToDoItem?> patchDocument);

        Task<bool> RemoveTaskAsync(Guid id);

        Task<IEnumerable<ToDoItem>> GetAllCompletedTasksAsync(bool? IsCompleted);

        Task<IEnumerable<ToDoItem>> GetAllPendingTasksAsync(bool? IsCompleted);

        Task<bool> TaskExistsAsync(Guid taskId);

        Task<bool> SaveChangesAsync();

    }

}