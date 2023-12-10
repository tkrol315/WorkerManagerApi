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
    public class GetManagerHandlerTests
    {
        private readonly Mock<IManagerRepository> _mockedRepository;
        private readonly IMapper _mapper;

        public GetManagerHandlerTests()
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
        public async System.Threading.Tasks.Task Handle_Returns_Manager_Success()
        {
            //arrange
            var managerId = Guid.NewGuid();
            var query = new GetManager(managerId);

            _mockedRepository.Setup(x => x.GetAsync(managerId)).ReturnsAsync(new Manager());

            var handler = new GetManagerHandler(_mockedRepository.Object, _mapper);


            //act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().BeOfType<GetManagerDto>();
            _mockedRepository.Verify(x => x.GetAsync(managerId), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Handle_Throws_UserNotFoundException_When_Manager_Does_Not_Exists()
        {
            //arrange
            var managerId = Guid.NewGuid();
            var query = new GetManager(managerId);

            //act
            var handler = new GetManagerHandler(_mockedRepository.Object, _mapper);
            var act = () => handler.Handle(query, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<UserNotFoundException>();
            _mockedRepository.Verify(r => r.GetAsync(managerId), Times.Once);
        }
    }
}
