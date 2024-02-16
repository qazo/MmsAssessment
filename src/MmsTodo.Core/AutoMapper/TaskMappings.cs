using AutoMapper;
using MmsTodo.Core.Common;
using MmsTodo.Core.Data.Models;

namespace MmsTodo.Core.AutoMapper;

public class TaskMappings : Profile
{
    public TaskMappings()
    {
        CreateMap<TodoTask, TaskDto>();
        CreateMap<CreateTaskDto, TodoTask>()
            .ForMember(dst => dst.IsCompleted, opt => opt.MapFrom(src => false))
            .ForMember(dst => dst.DateCompleted, opt => opt.MapFrom(src => (DateTimeOffset?)null))
            .ForMember(dst => dst.DateCreated, opt => opt.MapFrom(src => DateTimeOffset.Now));
    }
}