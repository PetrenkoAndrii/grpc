using GrpcService.Entities;
using GrpcService.Repositories.Interfaces;

namespace GrpcService.Repositories;

public class UserOrganizationRepository : IUserOrganizationRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserOrganizationRepository"/> class.
    /// </summary>
    /// <param name="context">AppDbContext</param>
    public UserOrganizationRepository(AppDbContext context)
    {
        _context = context;

    }
    public async Task<bool> AssociateUserToOrganizationAsync(UsersOrganizationsEntity entity)
    {
        /*var addedEntity =*/ await _context.UsersOrganizations.AddAsync(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
        //return addedEntity.Entity.Id;
    }
}
