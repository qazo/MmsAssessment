using MmsTodo.Core;
using MmsTodo.Core.Data;
using MmsTodo.Core.Services;

namespace MmsTodo.WebApi.Extensions;

public static class MmsTotoServiceExtensions
{
    public static IServiceCollection AddTodoServices(this IServiceCollection services)
    {
        services.AddScoped<TaskService>();
        services.AddDbContext<TodoDb>();
        services.AddAutoMapper(new[] { typeof(Program), typeof(Result) });
        return services;
    }
}