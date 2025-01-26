using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Task_Web_API.Entities;

namespace Task_Web_API.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly TaskDbContext _taskDbContext;

        private readonly IMapper _mapper;

        public ToDoRepository(TaskDbContext taskDbContext, IMapper mapper)
        {
            _taskDbContext = taskDbContext ?? throw new ArgumentNullException(nameof(taskDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ToDoItem>> GetAllTasksAsync()
        {
            return await _taskDbContext.ToDoItems.OrderBy(t => t.Title).AsNoTracking().ToListAsync();  //Add an (non-clusterd) index on the Title column in the database to improve performance.
        }
        public async Task<ToDoItem?> FindTaskByIdAsync(Guid taskId)
        {
            return await _taskDbContext.ToDoItems.FindAsync(taskId);
        }

        public async Task<Guid> AddTaskAsync(ToDoItem toDoItem) // Returning only the ID instead of Returning the full entity
        {
            await _taskDbContext.ToDoItems.AddAsync(toDoItem);
            await _taskDbContext.SaveChangesAsync();
            return toDoItem.Id; 
        }

        public async Task<ToDoItem?> UpdateTaskAsync(Guid taskId, ToDoItem taskToUpdate)
        {
            var existingTask = await FindTaskByIdAsync(taskId);

            _mapper.Map(taskToUpdate, existingTask);

            // existingTask.Title = toDoItem.Title;
            // existingTask.Description = toDoItem.Description;
            // existingTask.IsCompleted = toDoItem.IsCompleted;
            // existingTask.CreatedAt = DateTime.UtcNow; //toDoItem.CreatedAt;
            // existingTask.CompletedAt = toDoItem.CompletedAt;

            await _taskDbContext.SaveChangesAsync();

            return existingTask;
        }

        public async Task<bool> RemoveTaskAsync(Guid taskId)
        {
            var toDoItem = await FindTaskByIdAsync(taskId);

            if (toDoItem == null)
            {
                return false;
            }

            _taskDbContext.ToDoItems.Remove(toDoItem);

            return await _taskDbContext.SaveChangesAsync() > 0;
        }


        public async Task<bool> TaskExistsAsync(Guid taskId)
        {
            return await _taskDbContext.ToDoItems.AsNoTracking().AnyAsync(t => t.Id == taskId);
        }

        public async Task<IEnumerable<ToDoItem>> GetAllCompletedTasksAsync(bool? IsCompleted)
        {
            return await _taskDbContext.ToDoItems.Where(t => t.IsCompleted == true).AsNoTracking().ToListAsync();

        }

        public async Task<IEnumerable<ToDoItem>> GetAllPendingTasksAsync(bool? IsCompleted)
        {
            return await _taskDbContext.ToDoItems.Where(t => t.IsCompleted == false).AsNoTracking().ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _taskDbContext.SaveChangesAsync() >= 0;
        }


    }

}