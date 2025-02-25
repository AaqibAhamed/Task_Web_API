using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Task_Web_API.Entities;

namespace Task_Web_API.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly TaskDbContext _taskDbContext;

        public ToDoRepository(TaskDbContext taskDbContext)
        {
            _taskDbContext = taskDbContext ?? throw new ArgumentNullException(nameof(taskDbContext));
        }

        public async Task<IEnumerable<ToDoItem>> FindAllTasksAsync()
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

        public async Task<ToDoItem> UpdateTaskAsync(Guid taskId, ToDoItem taskToUpdate)
        {
            var existingTask = await FindTaskByIdAsync(taskId);

            // // Manually update the properties
            existingTask.Title = taskToUpdate.Title;
            existingTask.Description = taskToUpdate.Description;
            existingTask.IsCompleted = taskToUpdate.IsCompleted;
            existingTask.CreatedAt =  taskToUpdate.CreatedAt; //DateTime.UtcNow;
            existingTask.CompletedAt = taskToUpdate.CompletedAt;

            await _taskDbContext.SaveChangesAsync();

            return existingTask;
        }

        public async Task<ToDoItem?> ApplyPatchTaskAsync(Guid taskId, JsonPatchDocument<ToDoItem?> patchDocument)
        {
            var existingTask = await FindTaskByIdAsync(taskId);

            patchDocument.ApplyTo(existingTask);

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


        // public async Task<ToDoItem?> PatchAsyncUsingMapper(Guid taskId, JsonPatchDocument<ToDoItemUpdateDto> patchDocument)
        // {
        //     var existingTask = await FindTaskByIdAsync(taskId);

        //     // Map the existing entity to the DTO
        //     var toDoItemUpdateDto = _mapper.Map<ToDoItemUpdateDto>(existingTask);

        //     // Apply the patch to the DTO
        //     patchDocument.ApplyTo(toDoItemUpdateDto);

        //     await _taskDbContext.SaveChangesAsync();

        //     return existingTask;
        // }


    }

}