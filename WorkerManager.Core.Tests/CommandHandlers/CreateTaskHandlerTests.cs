using FluentAssertions;
using MediatR;
using Moq;
using WorkerManager.Application.Commands;
using WorkerManager.Application.Commands.Handlers;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Repositories;
using WorkerManager.Application.Services;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Core.Tests.CommandHandlers
{
    public class CreateTaskHandlerTests
    {
        private readonly Mock<IManagerRepository> _repositoryMock;
        private readonly Mock<IUserContextService> _userContextMock;
        private readonly Guid _managerId = new Guid();
        private readonly CreateTask _command;
        private readonly CreateTaskDto _taskDto;
        private readonly CreateTaskHandler _handler;
        public CreateTaskHandlerTests()
        {
            _repositoryMock = new();
            _userContextMock = new();
            _taskDto = new()
            {
                Name = "test",
                Description = "test"
            };
            _command = new(_managerId, _taskDto);
            _handler = new(_repositoryMock.Object,_userContextMock.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task Handle_Creates_New_Task_For_Valid_Input_Success()
        {
            //arrange 
            var manager = new Manager() { Id = _managerId };
            _repositoryMock.Setup(r => r.GetAsync(_managerId)).ReturnsAsync(manager);
            var newTask = new Domain.Entities.Task()
            {
                Name = _taskDto.Name,
                Description = _taskDto.Description,
            };
            _userContextMock.Setup(u => u.UserId).Returns(manager.Id);

            //act
            var result = await _handler.Handle(_command, CancellationToken.None);

            //assert
            
            result.Should().NotBeNull();
            result.Should().BeOfType<Unit>();
            _repositoryMock.Verify(r => r.GetAsync(_managerId), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(manager), Times.Once);
            _userContextMock.Verify(u => u.UserId, Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Handle_Throws_UserNotFoundException_When_Manager_Not_Found()
        {
            //arrange
            var manager = new Manager() { Id = Guid.NewGuid() };
            _repositoryMock.Setup(r => r.GetAsync(_managerId)).ReturnsAsync(default(Manager));

            //act
            var act = () => _handler.Handle(_command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<UserNotFoundException>();
            _repositoryMock.Verify(r => r.GetAsync(_managerId), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(manager), Times.Never);
            _userContextMock.Verify(u => u.UserId, Times.Never);
        }

        [Fact]
        public async System.Threading.Tasks.Task Handler_Throws_TaskAlreadyExistsException_When_Manager_Already_Has_Task_With_Same_Name()
        {
            //arrange
            var manager = new Manager()
            {
                Id = _managerId,
                Tasks = new()
                {
                    new()
                    {
                         Name = "test",
                        Description = "test"
                    }
                }
                
            };
            _repositoryMock.Setup(r => r.GetAsync(_managerId)).ReturnsAsync(manager);

            //act
            var act = ()=> _handler.Handle(_command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<TaskAlreadyExistsException>();
            _repositoryMock.Verify(r => r.GetAsync(_managerId), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(manager), Times.Never);
            _userContextMock.Verify(u => u.UserId, Times.Never);
        }
        [Fact]
        public async System.Threading.Tasks.Task Handle_CannotCreateTaskForOtherManagerException_When_Manager_Try_To_Create_Task_For_Another_Manager()
        {
            //arrange
            var manager = new Manager() { Id = _managerId };
            _repositoryMock.Setup(r => r.GetAsync(_managerId)).ReturnsAsync(manager);
            _userContextMock.Setup(r => r.UserId).Returns(Guid.NewGuid());

            //act
            var act = ()=> _handler.Handle(_command,CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<CannotCreateTaskForOtherManagerException>();
            _repositoryMock.Verify(r => r.GetAsync(_managerId), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(manager), Times.Never);
            _userContextMock.Verify(u => u.UserId, Times.Exactly(2));

        }
    }
}
