using Microsoft.Extensions.DependencyInjection;
using MmsTodo.Core.Data;
using MmsTodo.Core.Services;

namespace MmsTodo.Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<TodoDb>();
        services.AddScoped<TaskService>();
        services.AddAutoMapper(new[]{ typeof(TaskService)});
    }
}