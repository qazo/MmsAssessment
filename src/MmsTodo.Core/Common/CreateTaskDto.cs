namespace MmsTodo.Core.Common;

public class CreateTaskDto
{
    public required string Title { get; set; }
    public string? Description { get; set; }
}