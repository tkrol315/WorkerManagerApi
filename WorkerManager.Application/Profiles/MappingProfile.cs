using AutoMapper;
using WorkerManager.Application.Dto;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, GetUserDto>();

            CreateMap<Manager, GetManagerDto>()
            .IncludeBase<User, GetUserDto>();

            CreateMap<Worker, GetWorkerDto>()
            .IncludeBase<User, GetUserDto>();

            CreateMap<Domain.Entities.Task, GetTaskDto>();

            CreateMap<CreateTaskDto, Domain.Entities.Task>();

            CreateMap<RegisterUserDto, User>();
        }
    }
}
