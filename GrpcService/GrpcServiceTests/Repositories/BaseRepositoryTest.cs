using GrpcService.Entities;
using GrpcService;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceTests.Repositories;

public abstract class BaseRepositoryTest
{
    private const int DbRecordsCount = 5;

    private readonly bool InitUsers;
    private readonly bool InitOrganizations;
    private readonly bool InitUsersOrganizations;

    protected readonly AppDbContext context;

    public BaseRepositoryTest(
                            bool initUsers = false, 
                            bool initOrganizations = false,
                            bool initUsersOrganizations = false)
    {
        InitUsers = initUsersOrganizations || initUsers;
        InitOrganizations = initUsersOrganizations || initOrganizations;
        InitUsersOrganizations = initUsersOrganizations;

        var options = new DbContextOptionsBuilder<AppDbContext>()
                   .UseInMemoryDatabase("TestDatabase")
                   .Options;
        context = new AppDbContext(options);
    }

    [TestInitialize]
    public async Task InitializeAsync()
    {
        for (int i = 1; i <= DbRecordsCount; i++)
        {
            if (InitUsers)
            {
                await context.Users.AddAsync(
                    new UserEntity { Name = $"Name_{i}", UserName = $"UserName_{i}", Email = $"test_{i}@gmail.com" });
            }

            if (InitOrganizations)
            {
                await context.Organizations.AddAsync(
                    new OrganizationEntity { Address = "address", Name = $"name_{i}", IsDeleted = i % 2 == 0 });
            }

            if (InitUsersOrganizations)
            {
                await context.UsersOrganizations.AddAsync(
                    new UsersOrganizationsEntity { UserId = i, OrganizationId = DbRecordsCount - i == 0 ? DbRecordsCount : DbRecordsCount - i, IsDeleted = i % 2 == 0 });
            }
        }
        await context.SaveChangesAsync();
    }

    [TestCleanup]
    public void ClearDB()
    {
        if (InitUsersOrganizations)
            context.Database.EnsureDeleted();
    }
}
