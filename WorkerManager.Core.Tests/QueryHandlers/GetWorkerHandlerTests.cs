using AutoMapper;
using FluentAssertions;
using Moq;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Profiles;
using WorkerManager.Application.Queries;
using WorkerManager.Application.Queries.Handlers;
using WorkerManager.Application.Repositories;

namespace WorkerManager.Core.Tests.QueryHandlers
{
    public class GetWorkerHandlerTests
    {
        private readonly Mock<IWorkerRepository> _repositoryMock;
        private readonly IMapper _mapper;

        public GetWorkerHandlerTests()
        {
            _repositoryMock = new();
            var mapperCfg = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserMappingProfile>();
                cfg.AddProfile<TaskMappingProfile>();
            });
            _mapper = mapperCfg.CreateMapper();
        }

        [Fact]
        public async Task Handle_Returns_Worker_Success()
        {
            //arrange
            var workerId = Guid.NewGuid();
            var query = new GetWorker(workerId);
            _repositoryMock.Setup(r => r.GetAsync(workerId)).ReturnsAsync(new Domain.Entities.Worker() { Id = workerId });
            var handler = new GetWorkerHandler(_repositoryMock.Object, _mapper);

            //act
            var result = await handler.Handle(query, CancellationToken.None);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<GetWorkerDto>();
            _repositoryMock.Verify(r => r.GetAsync(workerId), Times.Once);
        }

        [Fact]
        public async Task Handle_Throws_UserNotFoundException_When_Worker_Not_Found()
        {
            //arrange
            var workerId = Guid.NewGuid();
            var query = new GetWorker(workerId);
            var handler = new GetWorkerHandler(_repositoryMock.Object, _mapper);

            //act
            var act = () => handler.Handle(query, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<UserNotFoundException>();
            _repositoryMock.Verify(r => r.GetAsync(workerId), Times.Once);
        }
    }
}
