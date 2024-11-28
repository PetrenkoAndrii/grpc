using GrpcService.Entities;
using GrpcService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrpcService.Repositories;

/// <summary>
/// Initializes a new instance of the <see cref="UserOrganizationRepository"/> class.
/// </summary>
/// <param name="context">AppDbContext</param>
public class UserOrganizationRepository(AppDbContext context) : BaseRepository(context), IUserOrganizationRepository
{
    public async Task<bool> AssociateUserToOrganizationAsync(UsersOrganizationsEntity entity)
    {
        await _context.UsersOrganizations.AddAsync(entity);
        return await IsSuccessfullSavedAsync();
    }

    public async Task<bool> DisassociateUserFromOrganizationAsync(int userId, int organizationId)
    {
        var userOrganizationEntity = await _context.UsersOrganizations
                                            .FirstOrDefaultAsync(uo => uo.UserId == userId &&
                                                uo.OrganizationId == organizationId &&
                                                !uo.IsDeleted);
        if (userOrganizationEntity == null)
            return false;

        userOrganizationEntity.IsDeleted = true;
        userOrganizationEntity.DeletedAt = DateTime.UtcNow;

        return await IsSuccessfullSavedAsync();
    }
}
