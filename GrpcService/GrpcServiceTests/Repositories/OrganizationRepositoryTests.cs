using GrpcService;
using GrpcService.Entities;
using GrpcService.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceTests.Repositories;

[TestClass]
public class OrganizationRepositoryTests
{
    private const int DbRecordsCount = 5;

    private readonly AppDbContext context;
    private readonly OrganizationRepository repository;

    public OrganizationRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
                   .UseInMemoryDatabase("TestDatabase")
                   .Options;
        context = new AppDbContext(options);

        repository = new(context);
    }

    [TestInitialize]
    public void Initialize()
    {
        for (int i = 1; i <= DbRecordsCount; i++)
        {
            context.Organizations.Add(
                new OrganizationEntity { Address = "address", Name = $"name_{i}", IsDeleted = i % 2 == 0 });
        }
        context.SaveChanges();
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
}
