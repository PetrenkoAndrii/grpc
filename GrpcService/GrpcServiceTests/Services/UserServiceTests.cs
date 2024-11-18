using AutoMapper;
using Grpc.Core;
using GrpcService;
using GrpcService.Entities;
using GrpcService.Repositories.Interfaces;
using GrpcService.Services;
using Moq;

namespace GrpcServiceTests.Services;

[TestClass]
public class UserServiceTests
{
    private readonly Mock<IUserRepository> repositoryMock = new();
    private readonly Mock<IMapper> mapperMock = new();

    private readonly UserService userService;

    public UserServiceTests()
    {
        userService = new(repositoryMock.Object, mapperMock.Object);
    }

    [TestMethod]
    public void GetUser_Success()
    {
        //Arrange
        repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(GetUserEntity());
        var request = new GetUserRequest { Id = 1 };

        //Act
        var result = userService.GetUser(request, It.IsAny<ServerCallContext>());

        //Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsCompletedSuccessfully);
    }

    [TestMethod]
    public async Task GetUser_NotSuccess()
    {
        //Arrange
        repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(It.IsAny<UserEntity>);
        var request = new GetUserRequest { Id = 105 };

        //Act
        var result = await userService.GetUser(request, It.IsAny<ServerCallContext>());

        //Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(string.IsNullOrEmpty(result.Name));
        Assert.IsTrue(string.IsNullOrEmpty(result.Username));
        Assert.IsTrue(string.IsNullOrEmpty(result.Email));
    }

    private static UserEntity GetUserEntity() =>
        new()
        {
            Name = "Andrew",
            UserName = "Hills",
            Email = "andrew@gmail.com"
        };
}
