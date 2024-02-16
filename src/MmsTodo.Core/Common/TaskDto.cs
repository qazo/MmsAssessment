namespace MmsTodo.Core.Common;

public class TaskDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTimeOffset? DateCompleted { get; set; }
}