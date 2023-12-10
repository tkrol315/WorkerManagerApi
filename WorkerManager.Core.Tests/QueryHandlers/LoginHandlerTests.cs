using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Queries;
using WorkerManager.Application.Queries.Handlers;
using WorkerManager.Application.Repositories;
using WorkerManager.Application.Services;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Core.Tests.QueryHandlers
{
    public class LoginHandlerTests
    {
        private readonly Mock<IUserRepository> _mockedRepository;
        private readonly Mock<IPasswordHasher<User>> _mockedPasswordHasher;
        private readonly Mock<IJwtService> _mockedIJwtService;

        public LoginHandlerTests()
        {
            _mockedRepository = new Mock<IUserRepository>();
            _mockedPasswordHasher = new Mock<IPasswordHasher<User>>();
            _mockedIJwtService = new Mock<IJwtService>();
        }

        [Fact]
        public async System.Threading.Tasks.Task Handle_Returns_Jwt_Token_Success()
        {
            //arrange
            var username = "test";
            var password = "test";
            var hashedPassword = "hashedPassword";
            var mockUser = new Mock<User>();
            var query = new Login(username, password);
            _mockedRepository.Setup(r => r
            .GetUserByNameAsync(username)).ReturnsAsync(mockUser.Object);
            _mockedPasswordHasher.Setup(ph => ph
            .VerifyHashedPassword(mockUser.Object, It.IsAny<string>(), It.IsAny<string>())).Returns(PasswordVerificationResult.Success);
            _mockedIJwtService.Setup(j => j.GetJwtToken(mockUser.Object)).Returns(It.IsAny<string>());
            var handler = new LoginHandler(_mockedRepository.Object, _mockedPasswordHasher.Object, _mockedIJwtService.Object);
            
            //act
            var result = await handler.Handle(query, CancellationToken.None);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<JwtTokenDto>();
            _mockedRepository.Verify(r => r.GetUserByNameAsync(username), Times.Once);
            _mockedPasswordHasher.Verify(ph => 
            ph.VerifyHashedPassword(mockUser.Object, It.IsAny<string>(), It.IsAny<string>()),Times.Once);
            _mockedIJwtService.Verify(j => j.GetJwtToken(mockUser.Object), Times.Once);
        }

        [Fact]  
        public async System.Threading.Tasks.Task Handle_Throws_InvalidUserNameOrPasswordException_When_Invalid_Username()
        {
            //arrange
            var username = "invalidUsername";
            var password = "test";
            var query = new Login(username, password);
            var handler = new LoginHandler(_mockedRepository.Object,_mockedPasswordHasher.Object,_mockedIJwtService.Object);

            //act
            var act = () => handler.Handle(query, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<InvalidUsernameOrPasswordException>();
            _mockedRepository.Verify(r => r.GetUserByNameAsync(username),Times.Once);
            _mockedPasswordHasher.Verify(ph => 
                ph.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), password), Times.Never);
            _mockedIJwtService.Verify(j => j.GetJwtToken(It.IsAny<User>()), Times.Never);    
        }

        [Fact]
        public async System.Threading.Tasks.Task Handle_Throws_InvalidUserNameOrPasswordException_When_Invalid_Password()
        {
            //arrange
            var username = "test";
            var password = "invalidPassword";
            var mockUser = new Mock<User>();
            var query = new Login(username, password);
            _mockedRepository.Setup(r => r.GetUserByNameAsync(username)).ReturnsAsync(mockUser.Object);
            var handler = new LoginHandler(_mockedRepository.Object, _mockedPasswordHasher.Object, _mockedIJwtService.Object);

            //act
            var act = () => handler.Handle(query, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<InvalidUsernameOrPasswordException>();
            _mockedRepository.Verify(r => r.GetUserByNameAsync(username), Times.Once);
            _mockedPasswordHasher.Verify(ph =>
                ph.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), password), Times.Once);
            _mockedIJwtService.Verify(j => j.GetJwtToken(It.IsAny<User>()), Times.Never);
        }
    }
}
