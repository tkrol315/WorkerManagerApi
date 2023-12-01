using AutoMapper;
using MediatR;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Exceptions;
using WorkerManager.Domain.Repositories;

namespace WorkerManager.Application.Queries.Handlers
{
    public class GetManagersTasksHandler : IRequestHandler<GetManagersTasks, IEnumerable<GetTaskDto>>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IMapper _mapper;

        public GetManagersTasksHandler(IManagerRepository managerRepository, IMapper mapper)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetTaskDto>> Handle(GetManagersTasks query, CancellationToken cancellationToken)
        {
            var managerExists = await _managerRepository.ExistsAsync(query.Id);
            if (!managerExists)
            {
                throw new UserNotFoundException(query.Id);
            }
            var tasks = _managerRepository.GetAsync(query.Id);
            return _mapper.Map<List<GetTaskDto>>(tasks);
        }
    }
}
