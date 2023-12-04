using AutoMapper;
using MediatR;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Repositories;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Application.Queries.Handlers
{
    public class GetManagersHandler : IRequestHandler<GetManagers, IEnumerable<GetManagerDto>>
    {
        private readonly IManagerRepository _repository;
        private readonly IMapper _mapper;

        public GetManagersHandler(IManagerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetManagerDto>> Handle(GetManagers _, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<List<GetManagerDto>>(await _repository.GetAllAsync());
            return result;     
        }
    }
}
