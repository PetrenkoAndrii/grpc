using AutoMapper;
using GrpcService.Entities;
using GrpcService.Models.Requests;
using GrpcService.Repositories.Interfaces;
using GrpcService.ServiceHandlers;
using Moq;

namespace GrpcServiceTests.ServiceHandlers;

[TestClass]
public class UserServiceHandlerTests
{
    private readonly Mock<IUserRepository> repositoryMock = new();
    private readonly Mock<IMapper> mapperMock = new();

    private readonly UserServiceHandler userServiceHandler;

    public UserServiceHandlerTests()
    {
        userServiceHandler = new(repositoryMock.Object, mapperMock.Object);
    }

    [TestMethod]
    public void GetUser_Success()
    {
        //Arrange
        repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(GetUserEntity());;

        //Act
        var result = userServiceHandler.GetUserAsync(1);

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

        //Act
        var result = await userServiceHandler.GetUserAsync(105);

        //Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task AddUser_NotUniqueUserName()
    {
        //Arrange
        const bool IsUniqueName = false;

        repositoryMock.Setup(r => r.IsUniqueUserNameAsync(It.IsAny<string>()))
                    .ReturnsAsync(IsUniqueName);

        var request = new AddUserRequest { UserName = "duplicated user name" };

        //Act
        var result = await userServiceHandler.AddUserAsync(request);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result > 0);
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

        var request = new AddUserRequest { Email = "duplicated email" };

        //Act
        var result = await userServiceHandler.AddUserAsync(request);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result > 0);
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
        var result = await userServiceHandler.AddUserAsync(request);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result > 0);
        Assert.IsTrue(result == AddedEntityId);
    }

    private static UserEntity GetUserEntity() =>
        new()
        {
            Name = "Andrew",
            UserName = "Hills",
            Email = "andrew@gmail.com"
        };
}
