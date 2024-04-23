
using System.ComponentModel.DataAnnotations;

public class TaskUpdateDto
{
    [Required(ErrorMessage = "You should provide a Title value.")]
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

}
