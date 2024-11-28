using GrpcService.Repositories;

namespace GrpcServiceTests.Repositories;

[TestClass]
public class UserOrganizationRepositoryTests : BaseRepositoryTest
{
    private readonly UserOrganizationRepository repository;

    public UserOrganizationRepositoryTests() : base(initUsersOrganizations: true)
    {
        repository = new(context);
    }

    [TestMethod]
    [Ignore("TODO: not covered")]
    public async Task Associate_NotSuccess()
    {

    }

    [TestMethod]
    [Ignore("TODO: not covered")]
    public async Task Associate_Success()
    {

    }

    [TestMethod]
    public async Task Disassociate_NotSuccess()
    {
        //Arrange
        const int UserId = 1;
        const int OrganizationId = 2;

        //Act
        var result = await repository.DisassociateUserFromOrganizationAsync(UserId, OrganizationId);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task Disassociate_Success()
    {
        //Arrange
        const int UserId = 1;
        const int OrganizationId = 4;

        //Act
        var result = await repository.DisassociateUserFromOrganizationAsync(UserId, OrganizationId);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result);
    }
}
