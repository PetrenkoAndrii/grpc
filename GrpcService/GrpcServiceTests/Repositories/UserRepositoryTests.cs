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
                new UserEntity { Name = $"Name_{i}", UserName = $"UserName_{i}", Email = $"test_{i}@gmail.com" });
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

    [TestMethod]
    public async Task IsUniqueUserName_Success()
    {
        //Arrange
        //Act
        var result = await repository.IsUniqueUserNameAsync("UserName");

        //Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task IsUniqueName_NotSuccess()
    {
        //Arrange
        //Act
        var result = await repository.IsUniqueUserNameAsync("UserName_1");

        //Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task IsUniqueEmail_Success()
    {
        //Arrange
        //Act
        var result = await repository.IsUniqueEmailAsync("andrew@gmail.com");

        //Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task IsUniqueEmail_NotSuccess()
    {
        //Arrange
        //Act
        var result = await repository.IsUniqueEmailAsync("test_1@gmail.com");

        //Assert
        Assert.IsFalse(result);
    }

    public async Task IsUserExist_Success()
    {
        //Arrange
        //Act
        var result = await repository.IsUserExistAsync(3);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task IsUserExist_NotSuccess()
    {
        //Arrange
        //Act
        var result = await repository.IsUserExistAsync(100);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result);
    }
}
