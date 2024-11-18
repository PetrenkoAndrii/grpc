using GrpcService.Entities;
using GrpcService.Repositories;
using GrpcService;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceTests.Repositories;

[TestClass]
public class UserRepositoryTests
{
    private const int DbRecordsCount = 5;

    private readonly AppDbContext context;
    private readonly UserRepository repository;

    public UserRepositoryTests()
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
            context.Users.Add(
                new UserEntity { Name = $"First Name_{i}", UserName = $"Last Name_{i}", Email = $"test_{i}@gmail.com" });
        }
        context.SaveChanges();
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
}
