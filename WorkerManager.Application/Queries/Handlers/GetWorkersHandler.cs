using AutoMapper;
using MediatR;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Repositories;

namespace WorkerManager.Application.Queries.Handlers
{
    public class GetWorkersHandler : IRequestHandler<GetWorkers, IEnumerable<GetWorkerDto>>
    {
        private readonly IWorkerRepository _repository;
        private readonly IMapper _mapper;

        public GetWorkersHandler(IWorkerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetWorkerDto>> Handle(GetWorkers _, CancellationToken cancellationToken)
        {
            var workers = await _repository.GetAllAsync();
            return _mapper.Map<List<GetWorkerDto>>(workers);

        }
    }
}
