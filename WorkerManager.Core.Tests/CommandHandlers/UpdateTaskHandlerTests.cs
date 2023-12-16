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
    public class UpdateTaskHandlerTests
    {
        private readonly Mock<IManagerRepository> _repositoryMock;
        private readonly Mock<IUserContextService> _userContextServiceMock;
        private readonly Guid _managerId = Guid.NewGuid();
        private readonly string _taskName = "test";
        private readonly string _description = "description";
        private readonly UpdateTaskDto _taskDto;
        private readonly UpdateTask _command;
        private readonly UpdateTaskHandler _handler;
        public UpdateTaskHandlerTests()
        {
            _repositoryMock = new();
            _userContextServiceMock = new();
            _taskDto = new() { Name = _taskName, Description = _description };
            _command = new(_managerId, _taskName, _taskDto);
            _handler = new(_repositoryMock.Object, _userContextServiceMock.Object);
        }
        [Fact]
        public async System.Threading.Tasks.Task Handle_Updates_Task_Success()
        {
            //arrange
            var manager = new Manager() { Id = _managerId, Tasks = new() { new() { Name = _taskName, Description = _description } } };
            _repositoryMock.Setup(r => r.GetAsync(_managerId)).ReturnsAsync(manager);
            _userContextServiceMock.Setup(u => u.UserId).Returns(_managerId);

            //act
            var result = await _handler.Handle(_command, CancellationToken.None);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Unit>();
            _repositoryMock.Verify(r => r.GetAsync(_managerId), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(manager), Times.Once);
            _userContextServiceMock.Verify(u => u.UserId, Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Handle_UserNotFoundException_When_Manager_Not_Found()
        {
            //arrange
            _repositoryMock.Setup(r => r.GetAsync(_managerId)).ReturnsAsync(default(Manager));

            //act 
            var act = () => _handler.Handle(_command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<UserNotFoundException>();
            _repositoryMock.Verify(r => r.GetAsync(_managerId), Times.Once);
            _userContextServiceMock.Verify(u => u.UserId, Times.Never);
        }
        [Fact]
        public async System.Threading.Tasks.Task Handle_TaskNotFoundException_When_Task_Is_Not_Found()
        {
            //arrange
            var manager = new Manager() { Id = _managerId };
            _repositoryMock.Setup(r => r.GetAsync(_managerId)).ReturnsAsync(manager);

            //act
            var act = () => _handler.Handle(_command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<TaskNotFoundException>();
            _repositoryMock.Verify(r => r.GetAsync(_managerId), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(manager), Times.Never);
            _userContextServiceMock.Verify(u => u.UserId, Times.Never);
        }
        [Fact]
        public async System.Threading.Tasks.Task Handle_UserIsNotCreatorException_When_Manager_Is_Not_Creator()
        {
            //arrange
            var manager = new Manager() { Id = _managerId, Tasks = new() { new() { Name = _taskName, Description = _description } } };
            _repositoryMock.Setup(r => r.GetAsync(_managerId)).ReturnsAsync(manager);
            
            //act
            var act = () => _handler.Handle(_command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<UserIsNotCreatorException>();
            _repositoryMock.Verify(r => r.GetAsync(_managerId), Times.Once);
            _userContextServiceMock.Verify(u => u.UserId, Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(manager), Times.Never);
        }
        
    }
}
