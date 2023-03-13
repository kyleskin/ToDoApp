using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models;

public class Todo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required] public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public bool InProgress { get; private set; } = true;

    public void MarkDone()
    {
        InProgress = false;
    }

    public void MarkInProgress()
    {
        InProgress = true;
    }
}