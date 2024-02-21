using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using MmsTodo.Core.Common;
using MmsTodo.Core.Data;
using MmsTodo.Core.Services;

namespace MmsTodo.Tests;

public class TodoTaskTests
{
    private readonly TaskService _taskService;
    private readonly TodoDb _dbContext;

    private void RefreshDb()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
    }

    public TodoTaskTests(TaskService taskService, TodoDb dbContext)
    {
        _taskService = taskService;
        _dbContext = dbContext;
        RefreshDb();
    }
    
    [Fact]
    public async Task Can_Create_Tasks()
    {
        var request = new CreateTaskDto
        {
            Title = "First Task",
            Description = "This is optional",
        };
        var result = await _taskService.CreateTask(request);
        Assert.True(result.IsSuccessful);
        Assert.Equal(1, result.Value!.Id);

        request = new()
        {
            Title = "Second Task",
            Description = null,
        };
        result = await _taskService.CreateTask(request);
        Assert.True(result.IsSuccessful);
        Assert.Equal(2, result.Value!.Id);

        result = await _taskService.FindTaskById(1);
        Assert.True(result.IsSuccessful);
        Assert.Equal("First Task", result.Value!.Title);
    }
    
    [Fact]
    public async Task Can_Retrieve_Task()
    {
        var request = new CreateTaskDto
        {
            Title = "First Task",
            Description = "This is optional",
        };
        var result = await _taskService.CreateTask(request);
        Assert.True(result.IsSuccessful);

        var taskResult = await _taskService.FindTaskById(1);
        Assert.True(taskResult.IsSuccessful);
        Assert.Equal("First Task", taskResult.Value!.Title);
    }
    
    
    [Fact]
    public async Task Can_Update_Task()
    {
        var createRequest = new CreateTaskDto
        {
            Title = "First Task",
            Description = "This is optional",
        };
        var result = await _taskService.CreateTask(createRequest);
        Assert.True(result.IsSuccessful);

        var updateRequest = new UpdateTaskDto
        {
            Id = result.Value!.Id,
            Title = "Name Changed"
        };
        var updateResult = await _taskService.UpdateTask(updateRequest);
        Assert.True(updateResult.IsSuccessful);
        Assert.Equal("Name Changed", updateResult.Value!.Title);

        var taskResult = await _taskService.FindTaskById(1);
        Assert.True(taskResult.IsSuccessful);
        Assert.Equal("Name Changed", taskResult.Value!.Title);
    }
    
    [Fact]
    public async Task Can_Complete_Task()
    {
        var createRequest = new CreateTaskDto
        {
            Title = "First Task",
            Description = "This is optional",
        };
        var result = await _taskService.CreateTask(createRequest);
        Assert.True(result.IsSuccessful);
        Assert.Null(result.Value!.DateCompleted);

        var completionResult = await _taskService.CompleteTask(result.Value!.Id);
        Assert.True(completionResult.IsSuccessful);

        var taskResult = await _taskService.FindTaskById(1);
        Assert.True(taskResult.IsSuccessful);
        Assert.True(taskResult.Value!.IsCompleted);
        Assert.NotNull(taskResult.Value!.DateCompleted);
    }

    [Fact]
    public async Task Can_Delete_Task()
    {
        var createRequest = new CreateTaskDto
        {
            Title = "To be deleted",
            Description = "Deletion test",
        };
        var result = await _taskService.CreateTask(createRequest);
        Assert.True(result.IsSuccessful);
        var newTask = result.Value!;

        result = await _taskService.FindTaskById(newTask.Id);
        Assert.True(result.IsSuccessful);
        var dbTask = result.Value!;
        
        Assert.Equal(newTask.Id, dbTask.Id);
        Assert.Equal(newTask.Title, dbTask.Title);
        Assert.Equal(newTask.Description, dbTask.Description);

        var deletionResult = await _taskService.DeleteTask(dbTask.Id);
        Assert.True(deletionResult.IsSuccessful);

        result = await _taskService.FindTaskById(dbTask.Id);
        Assert.False(result.IsSuccessful);
        Assert.Null(result.Value);
    }
}