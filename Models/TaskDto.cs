
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Task Get Dto
/// </summary>
public class TaskDto
{
    /// <summary>
    ///  The Id of the Task 
    /// </summary>
    public int Id { get; set; }

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
}
