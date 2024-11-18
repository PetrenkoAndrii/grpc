using AutoMapper;
using Grpc.Core;
using GrpcService;
using GrpcService.Entities;
using GrpcService.Repositories.Interfaces;
using GrpcService.Services;
using Moq;

namespace GrpcServiceTests.Services;

[TestClass]
public class OrganizationServiceTests
{
    private readonly Mock<IOrganizationRepository> repositoryMock = new();
    private readonly Mock<IMapper> mapperMock = new();

    private readonly OrganizationService organizationService;

    public OrganizationServiceTests()
    {
        organizationService = new(repositoryMock.Object, mapperMock.Object);
    }

    [TestMethod]
    public async Task AddOrganization_NotUniqueName()
    {
        //Arrange
        const bool IsUniqueName = false;

        repositoryMock.Setup(r => r.IsUniqueNameAsync(It.IsAny<string>()))
                    .ReturnsAsync(IsUniqueName);

        var request = new AddOrganizationRequest { Name = "duplicated name" };

        //Act
        var result = await organizationService.AddOrganization(request, It.IsAny<ServerCallContext>());

        //Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Id > 0);
    }

    [TestMethod]
    public async Task AddOrganization_Success()
    {
        //Arrange
        const bool IsUniqueName = true;
        const int AddedEntityId = 5;

        repositoryMock.Setup(r => r.IsUniqueNameAsync(It.IsAny<string>()))
                    .ReturnsAsync(IsUniqueName);
        repositoryMock.Setup(r => r.AddAsync(It.IsAny<OrganizationEntity>()))
                    .ReturnsAsync(AddedEntityId);

        var request = new AddOrganizationRequest { Name = "name", Address = "address" };

        //Act
        var result = await organizationService.AddOrganization(request, It.IsAny<ServerCallContext>());

        //Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Id > 0);
        Assert.IsTrue(result.Id == AddedEntityId);
    }
}
