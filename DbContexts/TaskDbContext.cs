using Microsoft.EntityFrameworkCore;
using Task_Web_API.Entities;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
    {
    }

    public DbSet<ToDoItem> ToDoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Add Index to Title Column
        builder.Entity<ToDoItem>()
            .HasIndex(t => t.Title)
            .HasDatabaseName("IX_ToDoItems_Title");

        // Fluent API Configuration
        builder.Entity<ToDoItem>()
            .Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Entity<ToDoItem>()
            .Property(t => t.CreatedAt) // If neither of the value is set
            .HasDefaultValueSql("GETUTCDATE()"); //the database default value (GETUTCDATE()) will be used when the entity is saved to the database.

        // Seed Initial Data
        builder.Entity<ToDoItem>().HasData(
            new ToDoItem("Buy Groceries") { Id = Guid.NewGuid(), Description = "Milk, Eggs, Bread", IsCompleted = false, CreatedAt = DateTime.UtcNow },
            new ToDoItem("Complete Project") { Id = Guid.NewGuid(), Description = "Finish coding and testing", IsCompleted = false }, //value set in the constructor (DateTime.UtcNow) will be used
            new ToDoItem("Workout") { Id = Guid.NewGuid(), Description = "Exercise for 30 minutes", IsCompleted = false, CreatedAt = new DateTime(2025, 1, 1) } //Explicitly Set Value:
        );


    }
}
