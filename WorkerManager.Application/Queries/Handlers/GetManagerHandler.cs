using AutoMapper;
using MediatR;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Repositories;

namespace WorkerManager.Application.Queries.Handlers
{
    public class GetManagerHandler : IRequestHandler<GetManager, GetManagerDto>
    {
        private readonly IManagerRepository _repository;
        private readonly IMapper _mapper;
        public GetManagerHandler(IManagerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetManagerDto> Handle(GetManager query, CancellationToken cancellationToken)
        {
            var manager = await _repository.GetAsync(query.ManagerId)
                ?? throw new UserNotFoundException(query.ManagerId);

            return _mapper.Map<GetManagerDto>(manager);
        }
    }
}
