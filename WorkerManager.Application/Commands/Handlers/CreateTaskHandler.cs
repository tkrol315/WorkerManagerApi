using AutoMapper;
using MediatR;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Repositories;

namespace WorkerManager.Application.Commands.Handlers
{
    public class CreateTaskHandler : IRequestHandler<CreateTask, Unit>
    {
        private readonly IManagerRepository _repository;
        private readonly IMapper _mapper;

        public CreateTaskHandler(IManagerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateTask command, CancellationToken cancellationToken)
        {
            var manager = await _repository.GetAsync(command.Id)
                ?? throw new UserNotFoundException(command.Id);

            var newTask = _mapper.Map<Domain.Entities.Task>(command.Dto);
            newTask.Manager = manager;
            newTask.TaskStatus = Domain.Enums.TaskStatus.NotAssigned;
            manager.Tasks.Add(newTask);
            await _repository.UpdateAsync(manager);
            return Unit.Value;
        }
    }
}
