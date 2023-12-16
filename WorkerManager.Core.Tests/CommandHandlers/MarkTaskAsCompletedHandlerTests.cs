using FluentAssertions;
using MediatR;
using Moq;
using WorkerManager.Application.Commands;
using WorkerManager.Application.Commands.Handlers;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Repositories;
using WorkerManager.Application.Services;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Core.Tests.CommandHandlers
{
    public class MarkTaskAsCompletedHandlerTests
    {
        private readonly Mock<IWorkerRepository> _repositoryMock;
        private readonly Mock<IUserContextService> _userContextMock;
        private readonly Guid _workerId = Guid.NewGuid();
        private readonly MarkTaskAsCompleted _command;
        private readonly MarkTaskAsCompletedHandler _handler;
        public MarkTaskAsCompletedHandlerTests()
        {
            _repositoryMock = new();
            _userContextMock = new();
            _command = new(_workerId);
            _handler = new(_repositoryMock.Object, _userContextMock.Object);
        }
        [Fact]
        public async System.Threading.Tasks.Task Handle_Marks_Task_As_Completed_As_Worker_Success()
        {
            //arrange
            var worker = new Worker()
            {
                Id = Guid.NewGuid(),
                AssignedTask = new Domain.Entities.Task() { Name = "test" }
            };
            _repositoryMock.Setup(r => r.GetAsync(_workerId)).ReturnsAsync(worker);
            _userContextMock.Setup(u => u.UserId).Returns(worker.Id);

            //act
            var result = await _handler.Handle(_command,CancellationToken.None);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Unit>();
            _repositoryMock.Verify(r => r.GetAsync(_workerId),Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(worker), Times.Once);
            _userContextMock.Verify(u => u.UserId,Times.Once);
        }
        [Fact]
        public async System.Threading.Tasks.Task Handle_Marks_Task_As_Completed_As_Creator_Manger_Success()
        {
            //arrange
            var managerId = Guid.NewGuid();
            var worker = new Worker()
            {
                Id = Guid.NewGuid(),
                AssignedTask = new Domain.Entities.Task() { Name = "test", ManagerId = managerId},
            };
            _repositoryMock.Setup(r => r.GetAsync(_workerId)).ReturnsAsync(worker);
            _userContextMock.Setup(u => u.UserId).Returns(managerId);

            //act
            var result = await _handler.Handle(_command, CancellationToken.None);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Unit>();
            _repositoryMock.Verify(r => r.GetAsync(_workerId), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(worker), Times.Once);
            _userContextMock.Verify(u => u.UserId, Times.Exactly(2));
        }

        [Fact]
        public async System.Threading.Tasks.Task Hnadle_Throws_UserNotFoundException_When_Worker_Not_Found()
        {
            //arrange
            _repositoryMock.Setup(r => r.GetAsync(_workerId)).ReturnsAsync(default(Worker));

            //act
            var act = ()=> _handler.Handle(_command,CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<UserNotFoundException>();
            _repositoryMock.Verify(r => r.GetAsync(_workerId), Times.Once);
            _userContextMock.Verify(u => u.UserId, Times.Never);
        }
        [Fact]
        public async System.Threading.Tasks.Task Handle_Thorws_AssignedTaskNotFoundException_When_Worker_Task_Null()
        {
            //arrange
            var worker = new Worker() { Id = _workerId};
            _repositoryMock.Setup(r => r.GetAsync(_workerId)).ReturnsAsync(worker);

            //act
            var act = ()=> _handler.Handle(_command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<AssignedTaskNotFoundException>();
            _repositoryMock.Verify(r => r.GetAsync(_workerId), Times.Once);
            _userContextMock.Verify(u => u.UserId, Times.Never);
            _repositoryMock.Verify(r => r.UpdateAsync(worker), Times.Never);
        }

        [Fact]
        public async System.Threading.Tasks.Task Handle_Throws_CannotMarkTaskAsCompletedException_When_Logged_User_Is_Not_Owner_And_Creator()
        {
            //arrange
            var taskCreatorId = Guid.NewGuid();
            var worker = new Worker() { Id = _workerId, AssignedTask = new() {Name = "test", ManagerId = taskCreatorId } };
            _repositoryMock.Setup(r => r.GetAsync(_workerId)).ReturnsAsync(worker);
            _userContextMock.Setup(u => u.UserId).Returns(Guid.NewGuid());

            //act
            var act = () => _handler.Handle(_command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<CannotMarkTaskAsCompletedException>();
            _repositoryMock.Verify(r => r.GetAsync(_workerId), Times.Once);
            _userContextMock.Verify(u => u.UserId, Times.Exactly(2));
            _repositoryMock.Verify(r => r.UpdateAsync(worker), Times.Never);
        }
     
    }
}
