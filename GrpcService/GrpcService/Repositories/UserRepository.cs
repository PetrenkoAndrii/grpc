using GrpcService.Entities;
using GrpcService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrpcService.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrganizationRepository"/> class.
    /// </summary>
    /// <param name="context">AppDbContext</param>
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserEntity> GetByIdAsync(int id)
    {
        var user = await _context.Users
                                   .FirstOrDefaultAsync(o => o.Id == id);
        return user!;
    }
}
