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
        private readonly Mock<IManagerRepository> _mockedRepository;
        private readonly IMapper _mapper;

        public GetManagersHandlerTests()
        {
            _mockedRepository = new Mock<IManagerRepository>();
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
            _mockedRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Manager>() {new(), new()});


            var handler = new GetManagersHandler(_mockedRepository.Object, _mapper);

            //act
            var result = await handler.Handle(query, CancellationToken.None);

            //assert
            result.Should().BeOfType<List<GetManagerDto>>();
            result.Should().HaveCount(2);
            _mockedRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
