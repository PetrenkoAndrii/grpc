using GrpcService.Entities;
using GrpcService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrpcService.Repositories;

/// <summary>
/// Initializes a new instance of the <see cref="UserRepository"/> class.
/// </summary>
/// <param name="context">AppDbContext</param>
public class UserRepository(AppDbContext context) : BaseRepository(context), IUserRepository
{
    public async Task<int> AddAsync(UserEntity user)
    {
        var addedEntity = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return addedEntity.Entity.Id;
    }

    public async Task<UserEntity> GetByIdAsync(int id)
    {
        var user = await _context.Users
                                   .FirstOrDefaultAsync(o => o.Id == id);
        return user!;
    }

    public async Task<bool> IsUniqueEmailAsync(string email)
    {
        var userEntity = await _context.Users.FirstOrDefaultAsync(o => o.Email == email);
        return userEntity == null;
    }

    public async Task<bool> IsUniqueUserNameAsync(string userName)
    {
        var userEntity = await _context.Users.FirstOrDefaultAsync(o => o.UserName == userName);
        return userEntity == null;
    }

    public async Task<bool> IsUserExistAsync(int id)
    {
        var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        return userEntity != null;
    }
}
