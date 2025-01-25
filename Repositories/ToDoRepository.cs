using Microsoft.EntityFrameworkCore;
using Task_Web_API.Entities;

namespace Task_Web_API.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly TaskDbContext _context;
        
        public ToDoRepository(TaskDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<ToDoItem>> GetAllTasksAsync()
        {
            return await _context.ToDoItems.OrderBy(t => t.Title).ToListAsync();  //Add an index on the Title column in the database to improve performance.

        }
        public async Task<ToDoItem?> GetTaskByIdAsync(int taskId)
        {
            return await _context.ToDoItems.Where(t => t.Id == taskId).FirstOrDefaultAsync();
        }

        public void AddTask(ToDoItem task)
        {
            _context.ToDoItems.Add(task);
        }

        public void DeleteTask(ToDoItem task)
        {
            _context.ToDoItems.Remove(task);
        }

        public async Task<bool> TaskExistsAsync(int taskId)
        {
            return await _context.ToDoItems.AnyAsync(t => t.Id == taskId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<IEnumerable<ToDoItem>> GetAllCompletedTasksAsync(bool? IsCompleted)
        {
            return await _context.ToDoItems.Where(t => t.IsCompleted == true).ToListAsync();

        }

        public async Task<IEnumerable<ToDoItem>> GetAllPendingTasksAsync(bool? IsCompleted)
        {
            return await _context.ToDoItems.Where(t => t.IsCompleted == false).ToListAsync();
        }

        public void UpdateTask(ToDoItem task)
        {
            throw new NotImplementedException();
        }

    }

}