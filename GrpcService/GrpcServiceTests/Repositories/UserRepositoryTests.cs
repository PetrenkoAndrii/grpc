using GrpcService.Repositories;

namespace GrpcServiceTests.Repositories;

[TestClass]
public class UserRepositoryTests : BaseRepositoryTest
{
    private readonly UserRepository repository;

    public UserRepositoryTests() : base(initUsers: true)
    {
        repository = new(context);
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
