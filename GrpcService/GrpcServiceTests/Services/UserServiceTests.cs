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

    [TestMethod]
    public async Task AddUser_NotUniqueUserName()
    {
        //Arrange
        const bool IsUniqueName = false;

        repositoryMock.Setup(r => r.IsUniqueUserNameAsync(It.IsAny<string>()))
                    .ReturnsAsync(IsUniqueName);

        var request = new AddUserRequest { Name = "duplicated user name" };

        //Act
        var result = await userService.AddUser(request, It.IsAny<ServerCallContext>());

        //Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Id > 0);
    }

    [TestMethod]
    public async Task AddUser_NotUniqueEmail()
    {
        //Arrange
        const bool IsUniqueName = true;
        const bool IsUniqueEmail = false;

        repositoryMock.Setup(r => r.IsUniqueUserNameAsync(It.IsAny<string>()))
                    .ReturnsAsync(IsUniqueName);
        repositoryMock.Setup(r => r.IsUniqueEmailAsync(It.IsAny<string>()))
                    .ReturnsAsync(IsUniqueEmail);

        var request = new AddUserRequest { Name = "duplicated email" };

        //Act
        var result = await userService.AddUser(request, It.IsAny<ServerCallContext>());

        //Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Id > 0);
    }

    [TestMethod]
    public async Task AddUser_Success()
    {
        //Arrange
        const bool IsUniqueName = true;
        const bool IsUniqueEmail = true;
        const int AddedEntityId = 5;

        repositoryMock.Setup(r => r.IsUniqueUserNameAsync(It.IsAny<string>()))
                    .ReturnsAsync(IsUniqueName);
        repositoryMock.Setup(r => r.IsUniqueEmailAsync(It.IsAny<string>()))
                    .ReturnsAsync(IsUniqueEmail);

        repositoryMock.Setup(r => r.AddAsync(It.IsAny<UserEntity>()))
                    .ReturnsAsync(AddedEntityId);

        var request = new AddUserRequest 
        { 
            Name = "name", 
            UserName = "username",
            Email = "goodemail@gmail.com"
        };

        //Act
        var result = await userService.AddUser(request, It.IsAny<ServerCallContext>());

        //Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Id > 0);
        Assert.IsTrue(result.Id == AddedEntityId);
    }

    private static UserEntity GetUserEntity() =>
        new()
        {
            Name = "Andrew",
            UserName = "Hills",
            Email = "andrew@gmail.com"
        };
}
