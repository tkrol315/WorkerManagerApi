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
    public class AssignTaskHandlerTests
    {
       
        private readonly Mock<IUserContextService> _userContextMock;
        private readonly Mock<IManagerRepository> _managerRepositoryMock;
        private readonly Mock<IWorkerRepository> _workerRepositoryMock;
        
        private readonly Guid _managerId = Guid.NewGuid();
        private readonly Guid _workerId = Guid.NewGuid();
        private readonly string _taskName = "sampleTaskName";
        private readonly AssignTask _command;
        private readonly AssignTaskHandler _handler;
        public AssignTaskHandlerTests()
        {
            _userContextMock = new Mock<IUserContextService>();
            _managerRepositoryMock = new Mock<IManagerRepository>();
            _workerRepositoryMock = new Mock<IWorkerRepository>();
            _command = new(_managerId,_workerId,_taskName);
            _handler = new(_managerRepositoryMock.Object, _workerRepositoryMock.Object,
                _userContextMock.Object);
        }
      

        [Fact]
        public async System.Threading.Tasks.Task Handle_Assigns_Task_To_The_Worker_Success()
        {
            //arrange
            var manager = new Manager()
            {
                Id = _managerId,
                Tasks = new()
                {
                    new()
                    {
                        Name = _taskName,
                    }
                }

            };
            var worker = new Worker()
            {
                Id = _workerId,
                AssignedTask = null,
            };
            _managerRepositoryMock.Setup(r => r.GetAsync(_managerId)).ReturnsAsync(manager);
            _workerRepositoryMock.Setup(r => r.GetAsync(_workerId)).ReturnsAsync(worker);
            _userContextMock.Setup(u => u.UserId).Returns(_managerId);

            //act
            var result = await _handler.Handle(_command,CancellationToken.None);

            //assert
            result.Should().Be(Unit.Value);
            worker.AssignedTask.Should().NotBeNull();
            worker.AssignedTask.Name.Should().Be(_command.TaskName);
            worker.AssignedTask.TaskStatus.Should().Be(Domain.Enums.TaskStatus.InProgress);
            _workerRepositoryMock.Verify(r => r.UpdateAsync(worker), Times.Once);
            _managerRepositoryMock.Verify(r => r.UpdateAsync(manager), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Handle_Throws_UserNotFoundException_When_Manager_Not_Found()
        {
            //arrange
            _managerRepositoryMock.Setup(r => r.GetAsync(_managerId)).ReturnsAsync(default(Manager));
            
            //act
            var act = () => _handler.Handle(_command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<UserNotFoundException>();
            
            _managerRepositoryMock.Verify(r => r.GetAsync(_managerId), Times.Once);
            _workerRepositoryMock.Verify(r => r.GetAsync(_workerId), Times.Never);
            _userContextMock.Verify(u => u.UserId, Times.Never);
        }
        
        [Fact]
        public async System.Threading.Tasks.Task Handle_Throws_TaskNotFoundException_When_Task_Not_Found_In_Managers_Tasks()
        {
            //arrange
            var manager = new Manager()
            {
                Id = _managerId,
                Tasks = new() 
            };
           
            _managerRepositoryMock.Setup(r => r.GetAsync(_managerId)).ReturnsAsync(manager);

            //act
            var act = () => _handler.Handle(_command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<TaskNotFoundException>();
            _managerRepositoryMock.Verify(r => r.GetAsync(_managerId), Times.Once);
            _workerRepositoryMock.Verify(r => r.GetAsync(_workerId), Times.Never);
            _userContextMock.Verify(u => u.UserId, Times.Never);

        }
        [Fact]
        public async System.Threading.Tasks.Task Handle_Thorws_TaskAlreadyAssignedException_When_Task_Is_Aleready_Assigned()
        {
            //arrange
            var manager = new Manager()
            {
                Id = _managerId,
                Tasks = new()
                {
                    new()
                    {
                        Name = _taskName,
                        WorkerId = Guid.NewGuid(),
                    }
                }

            };
            _managerRepositoryMock.Setup(r => r.GetAsync(_managerId)).ReturnsAsync(manager);

            //act
            var act = () => _handler.Handle(_command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<TaskAlreadyAssignedException>();
            _managerRepositoryMock.Verify(r => r.GetAsync(_managerId), Times.Once);
            _workerRepositoryMock.Verify(r => r.GetAsync(_workerId), Times.Never);
            _userContextMock.Verify(u => u.UserId, Times.Never);


        }

        [Fact]
        public async System.Threading.Tasks.Task Handle_Throws_UserNotFoundException_When_Worker_Not_Found()
        {
            //arrange
            var manager = new Manager()
            {
                Id = _managerId,
                Tasks = new()
                {
                    new()
                    {
                        Name = _taskName,
                    }
                }
            };
            _managerRepositoryMock.Setup(r => r.GetAsync(_managerId)).ReturnsAsync(manager);
            _workerRepositoryMock.Setup(r => r.GetAsync(_workerId)).ReturnsAsync(default(Worker));

            //act
            var act = () => _handler.Handle(_command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<UserNotFoundException>();

            _managerRepositoryMock.Verify(r => r.GetAsync(_managerId), Times.Once);
            _workerRepositoryMock.Verify(r => r.GetAsync(_workerId), Times.Once);
            _userContextMock.Verify(u => u.UserId, Times.Never);
        }
        [Fact]
        public async System.Threading.Tasks.Task Handle_Throws_WorkerHasAlreadyAssignedTask_When_Worker_AssignedTask_Is_Not_Null()
        { 
            //arrange
            var manager = new Manager()
            {
                Id = _managerId,
                Tasks = new()
                {
                    new()
                    {
                        Name = _taskName,
                    }
                }

            };
            var worker = new Worker()
            {
                Id = _workerId,
                AssignedTask = new Domain.Entities.Task()
            };
            _managerRepositoryMock.Setup(r => r.GetAsync(_managerId)).ReturnsAsync(manager);
            _workerRepositoryMock.Setup(r => r.GetAsync(_workerId)).ReturnsAsync(worker);

            //act
            var act = () => _handler.Handle(_command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<WorkerHasAlreadyAssignedTaskException>();

            _managerRepositoryMock.Verify(r => r.GetAsync(_managerId), Times.Once);
            _workerRepositoryMock.Verify(r => r.GetAsync(_workerId), Times.Once);
            _userContextMock.Verify(u => u.UserId, Times.Never);
        }
        [Fact]
        public async System.Threading.Tasks.Task Handle_Throws_TaskAssignmentNotAllowedException_When_Manager_Is_Not_Task_Creator()
        {
            //arrange
            var manager = new Manager()
            {
                Id = _managerId,
                Tasks = new()
                {
                    new()
                    {
                        Name = _taskName,
                    }
                }

            };
            var worker = new Worker()
            {
                Id = _workerId,
                AssignedTask = null
            };
            _managerRepositoryMock.Setup(r => r.GetAsync(_managerId)).ReturnsAsync(manager);
            _workerRepositoryMock.Setup(r => r.GetAsync(_workerId)).ReturnsAsync(worker);
            _userContextMock.Setup(u => u.UserId).Returns(Guid.NewGuid());

            //act
            var act = () => _handler.Handle(_command, CancellationToken.None);

            //assert
            var exception = await act.Should().ThrowAsync<TaskAssignmentNotAllowedException>();

            _managerRepositoryMock.Verify(r => r.GetAsync(_managerId), Times.Once);
            _workerRepositoryMock.Verify(r => r.GetAsync(_workerId), Times.Once);
            _userContextMock.Verify(u => u.UserId, Times.Once);
        }


    }
}
