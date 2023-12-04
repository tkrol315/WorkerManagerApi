using AutoMapper;
using MediatR;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Repositories;

namespace WorkerManager.Application.Queries.Handlers
{
    public class GetManagersTaskHandler : IRequestHandler<GetManagersTask, GetTaskManagerDto>
    {
 
        private readonly IManagerRepository _managerRepository;
        private readonly IMapper _mapper;

        public GetManagersTaskHandler(IManagerRepository managerRepository, IMapper mapper)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;
        }

        public async Task<GetTaskManagerDto> Handle(GetManagersTask query, CancellationToken cancellationToken)
        {
            var manager = await _managerRepository.GetAsync(query.Id)
                ?? throw new UserNotFoundException(query.Id);

            var task = manager.Tasks.FirstOrDefault(t => t.Name.ToLower() == query.TaskName.ToLower())
                ?? throw new TaskNotFoundException(query.TaskName);

            return _mapper.Map<GetTaskManagerDto>(task);
        }
    }
}
