namespace MmsTodo.Core.Common;

public class TaskSearchCriteriaDto
{
    public string? Title { get; set; }
    public bool? IsCompleted { get; set; }
    public DateTimeOffset? CompletionDateStart { get; set; }
    public DateTimeOffset? CompletionDateEnd { get; set; }
    public DateTimeOffset? CreateDateStart { get; set; }
    public DateTimeOffset? CreateDateEnd { get; set; }
}