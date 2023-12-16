using AutoMapper;
using FluentAssertions;
using Moq;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Profiles;
using WorkerManager.Application.Queries;
using WorkerManager.Application.Queries.Handlers;
using WorkerManager.Application.Repositories;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Core.Tests.QueryHandlers
{
    public class GetManagersHandlerTests
    {
        private readonly Mock<IManagerRepository> _repositoryMock;
        private readonly IMapper _mapper;

        public GetManagersHandlerTests()
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

        public async System.Threading.Tasks.Task Handle_Return_Managers_Success()
        {
            //arrange
            var query = new GetManagers();
            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Manager>() {new(), new()});


            var handler = new GetManagersHandler(_repositoryMock.Object, _mapper);

            //act
            var result = await handler.Handle(query, CancellationToken.None);

            //assert
            result.Should().BeOfType<List<GetManagerDto>>();
            result.Should().HaveCount(2);
            _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
