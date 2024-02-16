using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MmsTodo.Core.Data.Models;

[Table("Task")]
public class TodoTask
{
    [Key]
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTimeOffset? DateCompleted { get; set; }
    
    public DateTimeOffset DateCreated { get; set; }
}