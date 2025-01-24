using Microsoft.EntityFrameworkCore;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options): base(options)
    {
    }

    public DbSet<ToDoItem> Tasks { get;set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
            builder.Entity<ToDoItem>()
                 .HasData(
                new ToDoItem("Wake Up")
                {
                    Id = 1,
                    Description = "moring asdad",
                    IsCompleted = true
                },
                new ToDoItem("Answer the Nature")
                {
                    Id = 2,
                    Description = "go washroom",
                    IsCompleted = false
                },
                 new ToDoItem("Take Breakfast")
                {
                    Id = 3,
                    Description = "eat healthy food",
                    IsCompleted = false
                }); 
                
        base.OnModelCreating(builder);
    }
}
