
using Microsoft.EntityFrameworkCore;

public class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _context;
    public TaskRepository(TaskDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<ToDoItem>> GetAllTasksAsync()
    {
        return await _context.Tasks.ToListAsync(); // if required can orderby Title
    }
    public  async Task<ToDoItem?> GetTaskAsync(int taskId)
    {
       return await _context.Tasks.Where(t => t.Id == taskId).FirstOrDefaultAsync();
    }

    public void AddTask(ToDoItem task)
    {
        _context.Tasks.Add(task);
    }

    public void DeleteTask(ToDoItem task)
    {
        _context.Tasks.Remove(task);
    }

    public async Task<bool> TaskExistsAsync(int taskId)
    {
       return await _context.Tasks.AnyAsync(t => t.Id == taskId);
    }

    public async Task<bool> SaveChangesAsync()
    {
       return await _context.SaveChangesAsync() >= 0;
    }

    public async Task<IEnumerable<ToDoItem>> GetAllCompletedTasksAsync(bool? IsCompleted)
    {
        return await _context.Tasks.Where(t => t.IsCompleted == true ).ToListAsync();
      
    }

    public async Task<IEnumerable<ToDoItem>> GetAllPendingTasksAsync(bool? IsCompleted)
    {
         return await _context.Tasks.Where(t => t.IsCompleted == false ).ToListAsync();
    }


}