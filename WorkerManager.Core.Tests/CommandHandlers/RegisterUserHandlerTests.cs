using FluentAssertions;
using MediatR;
using Moq;
using WorkerManager.Application.Commands;
using WorkerManager.Application.Commands.Handlers;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Factories;
using WorkerManager.Application.Repositories;
using WorkerManager.Application.Services;
using WorkerManager.Domain.Entities;
using WorkerManager.Domain.Enums;

namespace WorkerManager.Core.Tests.CommandHandlers
{
    public class RegisterUserHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUserFactoryProviderService> _userFactoryProviderServiceMock;
        public RegisterUserHandlerTests()
        {
            _userRepositoryMock = new();
            _userFactoryProviderServiceMock = new();
        }
        [Fact]
        public async System.Threading.Tasks.Task Handle_Register_User_Success()
        {
            //arrange
            var userDto = new RegisterUserDto()
            {
                Username = "Test",
                Password = "Test",
                ConfirmPassword = "Test",
                RoleId = 1
            };
            _userRepositoryMock.Setup(r => r.AlreadyExistsByUserNameAsync(userDto.Username)).ReturnsAsync(false);
            var userFactoryMock = new Mock<IUserFactory>();
            userFactoryMock.Setup(f => f.CreateUser(userDto)).Returns(It.IsAny<Worker>());
            _userFactoryProviderServiceMock.Setup(f => f.GetFactory((Roles)userDto.RoleId)).Returns(userFactoryMock.Object);
            var command = new RegisterUser(userDto);
            var handler = new RegisterUserHandler(_userRepositoryMock.Object, _userFactoryProviderServiceMock.Object);
            
            //act
            var result = await handler.Handle(command, CancellationToken.None); 
            
            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Unit>();
            _userRepositoryMock.Verify(r => r.AlreadyExistsByUserNameAsync(userDto.Username), Times.Once);
            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
            _userFactoryProviderServiceMock.Verify(r => r.GetFactory((Roles)userDto.RoleId), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Handle_Throws_UserAlreadyExistsException_When_User_With_Same_Username_Already_Exists()
        {
            //arrange
            var userDto = new RegisterUserDto()
            {
                Username = "Test",
                Password = "Test",
                ConfirmPassword = "Test",
                RoleId = 1
            };
            _userRepositoryMock.Setup(r => r.AlreadyExistsByUserNameAsync(userDto.Username)).ReturnsAsync(true);
            var command = new RegisterUser(userDto);
            var handler = new RegisterUserHandler(_userRepositoryMock.Object, _userFactoryProviderServiceMock.Object);

            //act
            var act = ()=> handler.Handle(command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<UserWithUsernameAlreadyExistException>();
            _userRepositoryMock.Verify(r => r.AlreadyExistsByUserNameAsync(userDto.Username), Times.Once);
            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Never);
            _userFactoryProviderServiceMock.Verify(r => r.GetFactory((Roles)userDto.RoleId), Times.Never);
        }
        [Fact]
        public async System.Threading.Tasks.Task Handle_Throws_PasswordsDontMatchException_When_Password_And_ConfirmPassword_Are_Different()
        {
            //arrange
            var userDto = new RegisterUserDto()
            {
                Username = "Test",
                Password = "Test",
                ConfirmPassword = "Test123",
                RoleId = 1
            };
            _userRepositoryMock.Setup(r => r.AlreadyExistsByUserNameAsync(userDto.Username)).ReturnsAsync(false);
            var command = new RegisterUser(userDto);
            var handler = new RegisterUserHandler(_userRepositoryMock.Object, _userFactoryProviderServiceMock.Object);

            //act
            var act = () => handler.Handle(command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<PasswordsDontMatchException>();
            _userRepositoryMock.Verify(r => r.AlreadyExistsByUserNameAsync(userDto.Username), Times.Once);
            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Never);
            _userFactoryProviderServiceMock.Verify(r => r.GetFactory((Roles)userDto.RoleId), Times.Never);
        }

        [Fact]
        public async System.Threading.Tasks.Task Handle_Throws_RoleIdOutOfRangeException_When_Role_Id_Is_Invalid()
        {
            //arrange
            var userDto = new RegisterUserDto()
            {
                Username = "Test",
                Password = "Test",
                ConfirmPassword = "Test",
                RoleId = 3
            };
            _userRepositoryMock.Setup(r => r.AlreadyExistsByUserNameAsync(userDto.Username)).ReturnsAsync(false);
            _userFactoryProviderServiceMock.Setup(f => f.GetFactory((Roles)userDto.RoleId)).Throws<RoleIdOutOfRangeException>();
            var command = new RegisterUser(userDto);
            var handler = new RegisterUserHandler(_userRepositoryMock.Object, _userFactoryProviderServiceMock.Object);

            //act
            var act = () => handler.Handle(command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<RoleIdOutOfRangeException>();
            _userRepositoryMock.Verify(r => r.AlreadyExistsByUserNameAsync(userDto.Username), Times.Once);
            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Never);
            _userFactoryProviderServiceMock.Verify(r => r.GetFactory((Roles)userDto.RoleId), Times.Once);
        }
    }
}
