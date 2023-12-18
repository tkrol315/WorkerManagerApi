using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;
using WorkerManager.Application.Commands;
using WorkerManager.Application.Commands.Handlers;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Profiles;
using WorkerManager.Application.Repositories;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Core.Tests.CommandHandlers
{
    public class RegisterUserHandlerTests
    {
        private readonly Mock<IPasswordHasher<User>> _passwordHasherMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IMapper _mapper;
        public RegisterUserHandlerTests()
        {
            _passwordHasherMock = new();
            _userRepositoryMock = new();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<UserMappingProfile>());
            _mapper = mapperConfiguration.CreateMapper();
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
            _passwordHasherMock.Setup(p => p.HashPassword(It.IsAny<User>(), It.IsAny<string>())).Returns(It.IsAny<string>());
            var command = new RegisterUser(userDto);
            var handler = new RegisterUserHandler(_passwordHasherMock.Object, _mapper, _userRepositoryMock.Object);
            
            //act
            var result = await handler.Handle(command, CancellationToken.None); 
            
            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Unit>();
            _userRepositoryMock.Verify(r => r.AlreadyExistsByUserNameAsync(userDto.Username), Times.Once);
            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
            _passwordHasherMock.Verify(p => p.HashPassword(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
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
            var handler = new RegisterUserHandler(_passwordHasherMock.Object, _mapper, _userRepositoryMock.Object);

            //act
            var act = ()=> handler.Handle(command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<UserWithUsernameAlreadyExistException>();
            _userRepositoryMock.Verify(r => r.AlreadyExistsByUserNameAsync(userDto.Username), Times.Once);
            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Never);
            _passwordHasherMock.Verify(p => p.HashPassword(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
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
            var handler = new RegisterUserHandler(_passwordHasherMock.Object, _mapper, _userRepositoryMock.Object);

            //act
            var act = () => handler.Handle(command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<PasswordsDontMatchException>();
            _userRepositoryMock.Verify(r => r.AlreadyExistsByUserNameAsync(userDto.Username), Times.Once);
            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Never);
            _passwordHasherMock.Verify(p => p.HashPassword(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
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
            var command = new RegisterUser(userDto);
            var handler = new RegisterUserHandler(_passwordHasherMock.Object, _mapper, _userRepositoryMock.Object);

            //act
            var act = () => handler.Handle(command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<RoleIdOutOfRangeException>();
            _userRepositoryMock.Verify(r => r.AlreadyExistsByUserNameAsync(userDto.Username), Times.Once);
            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Never);
            _passwordHasherMock.Verify(p => p.HashPassword(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }
    }
}
