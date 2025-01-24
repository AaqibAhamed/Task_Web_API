using Task_Web_API.Services;

public class TaskService : ITaskService
{
    public void AddTask(Task task)
    {
        throw new NotImplementedException();
    }

    public void DeleteTask(Task task)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Task>> GetAllCompletedTasksAsync(bool? IsCompleted)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Task>> GetAllPendingTasksAsync(bool? IsCompleted)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Task>> GetAllTasksAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Task?> GetTaskAsync(int taskId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> TaskExistsAsync(int taskId)
    {
        throw new NotImplementedException();
    }

}