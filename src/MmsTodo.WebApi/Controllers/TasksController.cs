using Microsoft.AspNetCore.Mvc;
using MmsTodo.Core;
using MmsTodo.Core.Common;
using MmsTodo.Core.Services;

namespace MmsTodo.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;

    public TasksController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<List<TaskDto>> Get()
    {
        var results = await _taskService.GetActiveTasks();
        return results;
    }

    [HttpGet("{id}")]
    public async Task<Result<TaskDto>> Get(int id)
    {
        var result = await _taskService.FindTaskById(id);
        return result;
    }

    [HttpPost]
    public async Task<Result<TaskDto>> Post(CreateTaskDto model)
    {
        var result = await _taskService.CreateTask(model);
        return result;
    }

    [HttpPut]
    public async Task<Result<TaskDto>> Put(UpdateTaskDto model)
    {
        var result = await _taskService.UpdateTask(model);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<Result> Delete(int id)
    {
        var result = await _taskService.DeleteTask(id);
        return result;
    }

    [HttpPut("[action]/{id}")]
    public async Task<Result<TaskDto>> Complete(int id)
    {
        var result = await _taskService.CompleteTask(id);
        return result;
    }

    [HttpGet("[action]")]
    public async Task<List<TaskDto>> Search([FromQuery] TaskSearchCriteriaDto model)
    {
        var results = await _taskService.Search(model);
        return results;

    }
}