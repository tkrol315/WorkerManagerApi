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
    public class GetManagersTasksHandlerTests
    {
        private readonly Mock<IManagerRepository> _mockedRepository;
        private readonly IMapper _mapper;
        public GetManagersTasksHandlerTests()
        {
            _mockedRepository = new Mock<IManagerRepository>();
            var mapperConfig = new MapperConfiguration(cfg =>  
            {
                cfg.AddProfile<TaskMappingProfile>();
                cfg.AddProfile<UserMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }
        [Fact]
        public async System.Threading.Tasks.Task Handle_Returns_Tasks_Success()
        {
            //arrange
            var managerId = Guid.NewGuid();
            var query = new GetManagersTasks(managerId);
            var manager = new Manager()
            {
                Id = managerId,
                Tasks = new()
                {
                    new()
                    {
                        Name = "Sample-Task1",
                        Description = "Sample-Task1"
                    },
                    new()
                    {
                        Name = "Sample-Task2",
                        Description = "Sample-Task2"
                    }
                }
            };
            _mockedRepository.Setup(r => r.GetAsync(managerId)).ReturnsAsync(manager);
 
            var handler = new GetManagersTasksHandler(_mockedRepository.Object, _mapper);
            
            //act
            var result = await handler.Handle(query,CancellationToken.None);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<GetTaskManagerDto>>();
            result.Should().HaveCount(2);
            _mockedRepository.Verify(r => r.GetAsync(managerId), Times.Once);
        }
    }
}
