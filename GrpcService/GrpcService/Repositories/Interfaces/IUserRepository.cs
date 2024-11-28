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

    /// <summary>
    /// Insert User
    /// </summary>
    /// <param name="user">User to insert</param>
    /// <returns>The Id of the inserted User</returns>
    Task<int> AddAsync(UserEntity user);

    /// <summary>
    /// Returns is unique User Name via all users
    /// </summary>
    /// <param name="userName">User Name</param>
    /// <returns>True - if name is unique, False - if it isn't</returns>
    Task<bool> IsUniqueUserNameAsync(string userName);

    /// <summary>
    /// Returns is unique Email via all users
    /// </summary>
    /// <param name="email">User's email</param>
    /// <returns>True - if email is unique, False - if it isn't</returns>
    Task<bool> IsUniqueEmailAsync(string email);

    /// <summary>
    /// Returns is user exist via all users
    /// </summary>
    /// <param name="id">User's id</param>
    /// <returns>True - if user exist, False - if it isn't</returns>
    Task<bool> IsUserExistAsync(int id);
}
