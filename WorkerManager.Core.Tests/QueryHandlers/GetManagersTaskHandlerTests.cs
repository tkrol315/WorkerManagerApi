using AutoMapper;
using FluentAssertions;
using Moq;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Profiles;
using WorkerManager.Application.Queries;
using WorkerManager.Application.Queries.Handlers;
using WorkerManager.Application.Repositories;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Core.Tests.QueryHandlers
{
    public class GetManagersTaskHandlerTests
    {
        private readonly Mock<IManagerRepository> _repositoryMock;
        private readonly IMapper _mapper;
        public GetManagersTaskHandlerTests()
        {
            _repositoryMock = new();
            var mapperCfg = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile<UserMappingProfile>();
                    cfg.AddProfile<TaskMappingProfile>();
                });
            _mapper = mapperCfg.CreateMapper();
        }
        [Fact]
        public async System.Threading.Tasks.Task Handle_Returns_Task_Success()
        {
            //arrange
            var managerId = Guid.NewGuid();
            var taskName = "Sample-Name";
            var query = new GetManagersTask(managerId, taskName);

            var manager = new Manager()
            {
                Id = managerId,
                Tasks = new()
                {
                    new(){Name =  taskName, Description = "Sample-Task"}
                }
            };
            _repositoryMock.Setup(r => r.GetAsync(managerId)).ReturnsAsync(manager);
          
            var handler = new GetManagersTaskHandler(_repositoryMock.Object, _mapper);

            //act
            var result = await handler.Handle(query, CancellationToken.None);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<GetTaskManagerDto>();
            _repositoryMock.Verify(r => r.GetAsync(managerId), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Handle_Throws_UserNotFoundException_When_Manager_Does_Not_Exists()
        {
            //arrange
            var managerId = Guid.NewGuid();
            var taskName = "Sample-Task";
            var query = new GetManagersTask(managerId, taskName);
            var handler = new GetManagersTaskHandler(_repositoryMock.Object, _mapper);

            //act
            var act = () => handler.Handle(query, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<UserNotFoundException>();
            _repositoryMock.Verify(r => r.GetAsync(managerId), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Handle_Throws_TaskNotFoundException_When_Task_Does_Not_Exists()
        {
            //arrange
            var managerId = Guid.NewGuid();
            var taskName = "Sample-Task";
            var query = new GetManagersTask(managerId, taskName);
            _repositoryMock.Setup(r => r.GetAsync(managerId)).ReturnsAsync(new Manager());
            var handler = new GetManagersTaskHandler(_repositoryMock.Object, _mapper);

            //act
            var act = () => handler.Handle(query, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<TaskNotFoundException>();
            _repositoryMock.Verify(r => r.GetAsync(managerId), Times.Once);
        }
    }

}
