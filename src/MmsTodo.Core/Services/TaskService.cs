using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MmsTodo.Core.Common;
using MmsTodo.Core.Data;
using MmsTodo.Core.Data.Models;
using SQLitePCL;

namespace MmsTodo.Core.Services;
public class TaskService(TodoDb dbContext, ILogger<TaskService> logger, IMapper mapper)
{
    public async Task<Result<TaskDto>> CreateTask(CreateTaskDto model, CancellationToken cancellationToken = default)
    {
        try
        {
            var dbTask = mapper.Map<TodoTask>(model);
            await dbContext.Tasks.AddAsync(dbTask, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            var result = mapper.Map<TaskDto>(dbTask);
            return Result<TaskDto>.Success(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error when creating new task");
            return Result<TaskDto>.Fail(e);
        }
    }
    
    public async Task<List<TaskDto>> GetTasks(CancellationToken cancellationToken = default)
    {
        try
        {
            var dbTasks = await dbContext.Tasks
                .ToListAsync(cancellationToken);

            var results = mapper.Map<List<TaskDto>>(dbTasks);
            return results;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error when fetching tasks.");
            throw;
        }
    }

    public async Task<Result<TaskDto>> FindTaskById(int taskId, CancellationToken cancellationToken = default)
    {
        try
        {
            var dbTask = await dbContext.Tasks
                .Where(e => e.Id == taskId)
                .FirstOrDefaultAsync(cancellationToken);

            if (dbTask == null)
            {
                return Result<TaskDto>.Fail("Could not find task.");
            }

            var result = mapper.Map<TaskDto>(dbTask);
            return Result<TaskDto>.Success(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occured while attempting to find task");
            return Result<TaskDto>.Fail(e);
        }
    }
    public async Task<Result<TaskDto>> UpdateTask(UpdateTaskDto model, CancellationToken cancellationToken = default)
    {
        try
        {
            var dbTask = await dbContext.Tasks
                .Where(e => e.Id == model.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (dbTask == null)
            {
                return Result<TaskDto>.Fail("Could not find the task you wish to update.");
            }
            // Can we update completed tasks?
            dbTask.Title = model.Title;
            dbTask.Description = model.Description;
            await dbContext.SaveChangesAsync(cancellationToken);
            var result = mapper.Map<TaskDto>(dbTask);
            return Result<TaskDto>.Success(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed at updating task.");
            return Result<TaskDto>.Fail(e);
        }
    }

    public async Task<Result<TaskDto>> CompleteTask(int taskId, CancellationToken cancellationToken = default)
    {
        try
        {
            var dbTask = await dbContext.Tasks
                .Where(e => e.Id == taskId)
                .FirstOrDefaultAsync(cancellationToken);
            if (dbTask == null)
            {
                return Result<TaskDto>.Fail("Could not find task with given ID");
            }

            if (dbTask.IsCompleted)
            {
                return Result<TaskDto>.Fail("Task is already marked as complete.");
            }

            dbTask.IsCompleted = true;
            dbTask.DateCompleted = DateTimeOffset.Now;
            await dbContext.SaveChangesAsync(cancellationToken);
            var result = mapper.Map<TaskDto>(dbTask);
            return Result<TaskDto>.Success(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error when completing task.");
            return Result<TaskDto>.Fail(e);
        }
    }

    public async Task<Result> DeleteTask(int taskId, CancellationToken cancellationToken = default)
    {
        try
        {
            var task = await dbContext.Tasks
                .Where(e => e.Id == taskId)
                .FirstOrDefaultAsync(cancellationToken);
            if (task == null)
            {
                return Result.Fail("Could not find task with given ID");
            }

            dbContext.Tasks.Remove(task);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Could not delete task.");
            return Result.Fail(e);
        }
    }

    public async Task<List<TaskDto>> Search(TaskSearchCriteriaDto model, CancellationToken cancellationToken = default)
    {
        var dbTasks = await dbContext.Tasks
            .Where(e => model.Title == null || e.Title.Contains(model.Title))
            .Where(e => model.IsCompleted == null || e.IsCompleted == model.IsCompleted)
            .Where(e => model.CompletionDateStart == null || e.DateCompleted >= model.CompletionDateStart)
            .Where(e => model.CompletionDateEnd == null || e.DateCompleted < model.CompletionDateEnd)
            .Where(e => model.CreateDateStart == null || e.DateCompleted >= model.CreateDateStart)
            .Where(e => model.CreateDateEnd == null || e.DateCompleted < model.CreateDateEnd)
            .ToListAsync(cancellationToken);

        var results = mapper.Map<List<TaskDto>>(dbTasks);
        return results;
    }
}
