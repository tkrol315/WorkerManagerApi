using AutoMapper;
using WorkerManager.Application.Dto;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Application.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, GetUserDto>();

            CreateMap<Manager, GetManagerDto>()
            .IncludeBase<User, GetUserDto>();

            CreateMap<Worker, GetWorkerDto>()
            .IncludeBase<User, GetUserDto>();

            CreateMap<RegisterUserDto, Manager>();
            CreateMap<RegisterUserDto, Worker>();

        }
    }
}
