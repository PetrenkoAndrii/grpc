using GrpcService;
using GrpcService.Entities;
using GrpcService.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceTests.Repositories;

[TestClass]
public class OrganizationRepositoryTests : BaseRepositoryTest
{
    private readonly OrganizationRepository repository;

    public OrganizationRepositoryTests() : base(initOrganizations: true)
    {
        repository = new(context);
    }

    [TestMethod]
    public void AddOrganization_Success()
    {
        //Arrange
        var organization = new OrganizationEntity
        {
            Address = "address",
            Name = "name",
            IsDeleted = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            DeletedAt = null
        };

        //Act
        var result = repository.AddAsync(organization);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsCompletedSuccessfully);
    }

    [TestMethod]
    public async Task IsUniqueName_Success()
    {
        //Arrange
        //Act
        var result = await repository.IsUniqueNameAsync("test");

        //Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task IsUniqueName_NotSuccess()
    {
        //Arrange
        //Act
        var result = await repository.IsUniqueNameAsync("name_1");

        //Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task GetById_Success()
    {
        //Arrange
        //Act
        var result = await repository.GetByIdAsync(3);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsDeleted);
    }

    [TestMethod]
    public async Task GetById_NotSuccess()
    {
        //Arrange
        //Act
        var result = await repository.GetByIdAsync(100);

        //Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task Delete_Success()
    {
        //Arrange
        var organization = await repository.GetByIdAsync(1);
        organization.IsDeleted = true;
        organization.DeletedAt = DateTime.Now;

        //Act
        var result = await repository.DeleteAsync(organization);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task IsOrganizationExist_Success()
    {
        //Arrange
        //Act
        var result = await repository.IsOrganizationExistAsync(3);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task IsOrganizationExist_NotSuccess()
    {
        //Arrange
        //Act
        var result = await repository.IsOrganizationExistAsync(100);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result);
    }
}
