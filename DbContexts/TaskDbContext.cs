using Microsoft.EntityFrameworkCore;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options)
        : base(options)
    {
    }

    public DbSet<Task> Tasks { get;set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
            builder.Entity<Task>()
                 .HasData(
                new Task("Wake Up")
                {
                    Id = 1,
                    Description = "moring asdad",
                    IsCompleted = true
                },
                new Task("Answer the Nature")
                {
                    Id = 2,
                    Description = "go washroom",
                    IsCompleted = false
                },
                 new Task("Take Breakfast")
                {
                    Id = 3,
                    Description = "eat healthy food",
                    IsCompleted = false
                }); 
                
        base.OnModelCreating(builder);
    }
}
