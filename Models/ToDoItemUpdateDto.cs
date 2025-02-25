
using System.ComponentModel.DataAnnotations;

namespace Task_Web_API.Models
{

    /// <summary>
    /// Task Update Dto
    /// </summary>
    public class ToDoItemUpdateDto
    {
        /// <summary>
        ///  The Title of the Task 
        /// </summary>
        [Required(ErrorMessage = "You should provide a Title value.")]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        ///  The description of the Task
        /// </summary>
        [MaxLength(200)]
        public string? Description { get; set; }

        /// <summary>
        ///  Is Task completed or not 
        /// </summary>
        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

    }
}