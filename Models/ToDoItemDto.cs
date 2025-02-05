namespace Task_Web_API.Models
{

    /// <summary>
    /// Task Get Dto
    /// </summary>
    public class ToDoItemDto
    {
        /// <summary>
        ///  The Id of the Task 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  The Title of the Task 
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The description of the Task
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        ///   Is Task completed or not 
        /// </summary>
        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }
    }
}