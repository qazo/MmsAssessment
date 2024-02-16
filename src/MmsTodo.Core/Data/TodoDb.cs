using Microsoft.EntityFrameworkCore;
using MmsTodo.Core.Data.Models;

namespace MmsTodo.Core.Data;

public class TodoDb(string dbPath) : DbContext
{
    private string DbPath { get; set; } = dbPath;

    public TodoDb() : this("todo.db")
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
    
    public DbSet<TodoTask> Tasks { get; set; }
}