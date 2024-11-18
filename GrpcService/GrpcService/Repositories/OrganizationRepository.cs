using GrpcService.Entities;
using GrpcService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrpcService.Repositories;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrganizationRepository"/> class.
    /// </summary>
    /// <param name="context">AppDbContext</param>
    public OrganizationRepository(AppDbContext context)
    {
        _context = context;
    }

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
        var organization = await _context.Organizations.FirstOrDefaultAsync(o => o.Id == id);
        return organization!;
    }

    public async Task<bool> DeleteAsync(OrganizationEntity organization)
    {
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}
