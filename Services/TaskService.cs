using Task_Web_API.Services;

public class TaskService : ITaskService
{
    public void AddTask(ToDoItem task)
    {
        throw new NotImplementedException();
    }

    public void DeleteTask(ToDoItem task)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ToDoItem>> GetAllCompletedTasksAsync(bool? IsCompleted)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ToDoItem>> GetAllPendingTasksAsync(bool? IsCompleted)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ToDoItem>> GetAllTasksAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ToDoItem?> GetTaskAsync(int taskId)
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