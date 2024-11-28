using GrpcService.Entities;
using GrpcService.Models.Requests;
using GrpcService.Repositories.Interfaces;
using GrpcService.ServiceHandlers;
using Moq;

namespace GrpcServiceTests.ServiceHandlers;

[TestClass]
public class UserOrganizationAssociationServiceHandlerTests
{
    private readonly Mock<IUserRepository> userRepositoryMock = new();
    private readonly Mock<IOrganizationRepository> organizationRepositoryMock = new();
    private readonly Mock<IUserOrganizationRepository> userOrganizationRepositoryMock = new();

    private readonly UserOrganizationAssociationServiceHandler serviceHandler;

    public UserOrganizationAssociationServiceHandlerTests()
    {
        serviceHandler = new(userRepositoryMock.Object, organizationRepositoryMock.Object, userOrganizationRepositoryMock.Object);
    }

    [TestMethod]
    public async Task AssociateUserToOrganization_NotExistUser()
    {
        //Arrange
        const bool IsUserExist = false;

        userRepositoryMock.Setup(r => r.IsUserExistAsync(It.IsAny<int>()))
            .ReturnsAsync(IsUserExist);

        var request = new UserOrganizationAssociationRequest { UserId = 100, OrganizationId = 1 };

        //Act
        var result = await serviceHandler.AssociateUserToOrganizationAsync(request);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task AssociateUserToOrganization_NotExistOrganization()
    {
        //Arrange
        const bool IsUserExist = true;
        const bool IsOrganizationExist = false;

        userRepositoryMock.Setup(r => r.IsUserExistAsync(It.IsAny<int>()))
            .ReturnsAsync(IsUserExist);
        organizationRepositoryMock.Setup(r => r.IsOrganizationExistAsync(It.IsAny<int>()))
            .ReturnsAsync(IsOrganizationExist);

        var request = new UserOrganizationAssociationRequest { UserId = 1, OrganizationId = 100 };

        //Act
        var result = await serviceHandler.AssociateUserToOrganizationAsync(request);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task AssociateUserToOrganization_NotSuccess()
    {
        //Arrange
        const bool IsUserExist = true;
        const bool IsOrganizationExist = true;
        const bool IsAssociationSuccess = false;

        userRepositoryMock.Setup(r => r.IsUserExistAsync(It.IsAny<int>()))
            .ReturnsAsync(IsUserExist);
        organizationRepositoryMock.Setup(r => r.IsOrganizationExistAsync(It.IsAny<int>()))
            .ReturnsAsync(IsOrganizationExist);
        userOrganizationRepositoryMock.Setup(r => r.AssociateUserToOrganizationAsync(It.IsAny<UsersOrganizationsEntity>()))
            .ReturnsAsync(IsAssociationSuccess);

        var request = new UserOrganizationAssociationRequest { UserId = 1, OrganizationId = 1 };

        //Act
        var result = await serviceHandler.AssociateUserToOrganizationAsync(request);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task AssociateUserToOrganization_Success()
    {
        //Arrange
        const bool IsUserExist = true;
        const bool IsOrganizationExist = true;
        const bool IsAssociationSuccess = true;

        userRepositoryMock.Setup(r => r.IsUserExistAsync(It.IsAny<int>()))
            .ReturnsAsync(IsUserExist);
        organizationRepositoryMock.Setup(r => r.IsOrganizationExistAsync(It.IsAny<int>()))
            .ReturnsAsync(IsOrganizationExist);
        userOrganizationRepositoryMock.Setup(r => r.AssociateUserToOrganizationAsync(It.IsAny<UsersOrganizationsEntity>()))
            .ReturnsAsync(IsAssociationSuccess);

        var request = new UserOrganizationAssociationRequest { UserId = 1, OrganizationId = 1 };

        //Act
        var result = await serviceHandler.AssociateUserToOrganizationAsync(request);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result);
    }
}
