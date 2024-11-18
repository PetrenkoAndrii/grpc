using GrpcService.Entities;

namespace GrpcService.Repositories.Interfaces;

public interface IUserRepository
{
    /// <summary>
    /// Returns User by id
    /// </summary>
    /// <param name="id">The id of the User to look for</param>
    /// <returns>User entity</returns>
    Task<UserEntity> GetByIdAsync(int id);
}
