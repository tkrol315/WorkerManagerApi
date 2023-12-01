using AutoMapper;
using MediatR;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Exceptions;
using WorkerManager.Domain.Repositories;

namespace WorkerManager.Application.Queries.Handlers
{
    public class GetWorkerHandler : IRequestHandler<GetWorker, GetWorkerDto>
    {
        private readonly IWorkerRepository _repository;
        private readonly IMapper _mapper;

        public GetWorkerHandler(IWorkerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetWorkerDto> Handle(GetWorker query, CancellationToken cancellationToken)
        {
            var worker = await _repository.GetAsync(query.Id)
                ?? throw new UserNotFoundException(query.Id);

            return _mapper.Map<GetWorkerDto>(worker);
        }
    }
}
