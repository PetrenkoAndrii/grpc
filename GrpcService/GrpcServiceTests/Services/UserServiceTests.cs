using AutoMapper;
using Grpc.Core;
using GrpcService;
using GrpcService.Models.Responses;
using GrpcService.ServiceHandlers.Interfaces;
using GrpcService.Services;
using Request = GrpcService.Models.Requests;
using Moq;

namespace GrpcServiceTests.Services;

[TestClass]
public class UserServiceTests
{
    private readonly Mock<IUserServiceHandler> userServiceHandlerMock = new();
    private readonly Mock<IUserOrganizationAssociationServiceHandler> userOrganizationAssociationServiceHandlerMock = new();
    private readonly Mock<IMapper> mapperMock = new();

    private readonly UserService userService;

    public UserServiceTests()
    {
        userService = new(userServiceHandlerMock.Object, userOrganizationAssociationServiceHandlerMock.Object, mapperMock.Object);
    }

    [TestMethod]
    public void GetUser_Success()
    {
        //Arrange
        userServiceHandlerMock.Setup(r => r.GetUserAsync(It.IsAny<int>()))
            .ReturnsAsync(GetUserResponse());
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
        userServiceHandlerMock.Setup(r => r.GetUserAsync(It.IsAny<int>()))
            .ReturnsAsync(It.IsAny<GetUserResponse>);
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
        const int NotInsertedIdValue = -1;
        userServiceHandlerMock.Setup(r => r.AddUserAsync(It.IsAny<Request.AddUserRequest>()))
                    .ReturnsAsync(NotInsertedIdValue);

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
        const int NotInsertedIdValue = -1;
        userServiceHandlerMock.Setup(r => r.AddUserAsync(It.IsAny<Request.AddUserRequest>()))
                    .ReturnsAsync(NotInsertedIdValue);

        var request = new AddUserRequest { Email = "duplicated email" };

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
        const int AddedEntityId = 5;

        userServiceHandlerMock.Setup(r => r.AddUserAsync(It.IsAny<Request.AddUserRequest>()))
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

    [TestMethod]
    public async Task AssociateUserToOrganization_NotSuccess()
    {
        //Arrange
        const bool IsAssociationSuccess = false;

        userOrganizationAssociationServiceHandlerMock.Setup(r => r.AssociateUserToOrganizationAsync(It.IsAny<Request.UserOrganizationAssociationRequest>()))
                    .ReturnsAsync(IsAssociationSuccess);

        var request = new UserOrganizationAssociationRequest
        {
            UserId = 1,
            OrganizationId = 1
        };

        //Act
        var result = await userService.AssociateUserToOrganization(request, It.IsAny<ServerCallContext>());

        //Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task AssociateUserToOrganization_Success()
    {
        //Arrange
        const bool IsAssociationSuccess = true;

        userOrganizationAssociationServiceHandlerMock.Setup(r => r.AssociateUserToOrganizationAsync(It.IsAny<Request.UserOrganizationAssociationRequest>()))
                    .ReturnsAsync(IsAssociationSuccess);

        var request = new UserOrganizationAssociationRequest
        {
            UserId = 1,
            OrganizationId = 1
        };

        //Act
        var result = await userService.AssociateUserToOrganization(request, It.IsAny<ServerCallContext>());

        //Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);
    }

    private static GetUserResponse GetUserResponse() =>
        new()
        {
            Name = "Andrew",
            UserName = "Hills",
            Email = "andrew@gmail.com"
        };
}
