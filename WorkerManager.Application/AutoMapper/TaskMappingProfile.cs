using AutoMapper;
using WorkerManager.Application.Dto;

namespace WorkerManager.Application.Profiles
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<Domain.Entities.Task, GetTaskManagerDto>();
            CreateMap<Domain.Entities.Task, GetTaskWorkerDto>();
        }
    }
}
