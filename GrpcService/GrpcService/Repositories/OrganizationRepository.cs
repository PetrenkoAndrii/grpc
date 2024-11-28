using GrpcService.Entities;
using GrpcService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrpcService.Repositories;

/// <summary>
/// Initializes a new instance of the <see cref="OrganizationRepository"/> class.
/// </summary>
/// <param name="context">AppDbContext</param>
public class OrganizationRepository(AppDbContext context) : BaseRepository(context), IOrganizationRepository
{
    public async Task<int> AddAsync(OrganizationEntity organization)
    {
        var addedEntity = await _context.Organizations.AddAsync(organization);
        await _context.SaveChangesAsync();

        return addedEntity.Entity.Id;
    }

    public async Task<bool> IsUniqueNameAsync(string name)
    {
        var entity = await _context.Organizations.FirstOrDefaultAsync(o => o.Name == name);
        return entity == null;
    }

    public async Task<OrganizationEntity> GetByIdAsync(int id)
    {
        var organization = await _context.Organizations
                                        .Include(o => o.UsersOrganizations.Where(uo => !uo.IsDeleted))
                                        .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
        return organization!;
    }

    public async Task<bool> DeleteAsync(OrganizationEntity organization) =>
        await IsSuccessfullSavedAsync();

    public async Task<bool> IsOrganizationExistAsync(int id)
    {
        var entity = await _context.Organizations.FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
        return entity != null;
    }
}
