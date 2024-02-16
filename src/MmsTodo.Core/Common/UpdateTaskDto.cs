namespace MmsTodo.Core.Common;

public class UpdateTaskDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
}