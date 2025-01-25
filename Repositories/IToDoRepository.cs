using Task_Web_API.Entities;

namespace Task_Web_API.Repositories
{
    public interface IToDoRepository
    {
        Task<IEnumerable<ToDoItem>> GetAllTasksAsync();

        Task<ToDoItem?> GetTaskByIdAsync(Guid taskId);

        Task<IEnumerable<ToDoItem>> GetAllCompletedTasksAsync(bool? IsCompleted);

        Task<IEnumerable<ToDoItem>> GetAllPendingTasksAsync(bool? IsCompleted);

        Task<bool> TaskExistsAsync(Guid taskId);

        void AddTask(ToDoItem task);

        void UpdateTask(ToDoItem task);

        void DeleteTask(ToDoItem task);

        Task<bool> SaveChangesAsync();

    }

}