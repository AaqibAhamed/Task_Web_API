public interface ITaskRepository
{
    Task<IEnumerable<ToDoItem>> GetAllTasksAsync();

    Task<ToDoItem?> GetTaskAsync(int taskId);

    Task<IEnumerable<ToDoItem>> GetAllCompletedTasksAsync(bool? IsCompleted);

    Task<IEnumerable<ToDoItem>> GetAllPendingTasksAsync(bool? IsCompleted);

    Task<bool> TaskExistsAsync(int taskId);

    void AddTask(ToDoItem task);

    void DeleteTask(ToDoItem task);

    Task<bool> SaveChangesAsync();

}