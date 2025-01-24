public interface ITaskRepository
{
    Task<IEnumerable<Task>> GetAllTasksAsync();

    Task<Task?> GetTaskAsync(int taskId);

    Task<IEnumerable<Task>> GetAllCompletedTasksAsync(bool? IsCompleted);

    Task<IEnumerable<Task>> GetAllPendingTasksAsync(bool? IsCompleted);

    Task<bool> TaskExistsAsync(int taskId);

    void AddTask(Task task);

    void DeleteTask(Task task);

    Task<bool> SaveChangesAsync();

}