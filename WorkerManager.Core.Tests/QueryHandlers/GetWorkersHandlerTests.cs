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
    public class GetWorkersHandlerTests
    {
        private readonly Mock<IWorkerRepository> _mockedRepository;
        private readonly IMapper _mapper;

        public GetWorkersHandlerTests()
        {
            _mockedRepository = new Mock<IWorkerRepository>();
            var mapperCfg = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserMappingProfile>();
                cfg.AddProfile<TaskMappingProfile>();
            });
            _mapper = mapperCfg.CreateMapper();
        }
        [Fact]
        public async System.Threading.Tasks.Task Handle_Returns_Workers_Success()
        {
            //arrange
            var query = new GetWorkers();
            var listOfWorkers = new List<Worker>() { new(), new()};
            _mockedRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(listOfWorkers);
            var handler = new GetWorkersHandler(_mockedRepository.Object, _mapper);
            
            //act
            var result = await handler.Handle(query,CancellationToken.None);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<GetWorkerDto>>();
            result.Should().HaveCount(2);
            _mockedRepository.Verify(r => r.GetAllAsync(), Times.Once);   
        }
    }

}
