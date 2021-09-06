using AutoMapper;

using TasksApp.Data.Dtos;
using TasksApp.Data.Models;

namespace TasksApp.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Task, TaskResponseDto>();
            CreateMap<TaskCreateDto, Task>();
            CreateMap<TaskUpdateDto, Task>();
        }
    }
}
