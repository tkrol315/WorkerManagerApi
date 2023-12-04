using AutoMapper;
using MediatR;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Repositories;

namespace WorkerManager.Application.Queries.Handlers
{
    public class GetManagersTasksHandler : IRequestHandler<GetManagersTasks, IEnumerable<GetTaskManagerDto>>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IMapper _mapper;

        public GetManagersTasksHandler(IManagerRepository managerRepository, IMapper mapper)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetTaskManagerDto>> Handle(GetManagersTasks query, CancellationToken cancellationToken)
        {
            var manager = await _managerRepository.GetAsync(query.Id)
                ?? throw new UserNotFoundException(query.Id);

            var tasks = manager.Tasks;
            return _mapper.Map<List<GetTaskManagerDto>>(tasks);
        }
    }
}
