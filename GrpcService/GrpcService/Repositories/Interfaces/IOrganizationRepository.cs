using GrpcService.Entities;

namespace GrpcService.Repositories.Interfaces;

public interface IOrganizationRepository
{
    /// <summary>
    /// Insert Organization
    /// </summary>
    /// <param name="organization">Organization to insert</param>
    /// <returns>The Id of the inserted Organization</returns>
    Task<int> AddAsync(OrganizationEntity organization);

    /// <summary>
    /// Returns is unique name via all organizations
    /// </summary>
    /// <param name="name">Name of Organization</param>
    /// <returns>True - if name is unique, False - if it isn't</returns>
    Task<bool> IsUniqueNameAsync(string name);

    /// <summary>
    /// Returns Organization by id
    /// </summary>
    /// <param name="id">The id of the Organization to look for</param>
    /// <returns>Organization entity</returns>
    Task<OrganizationEntity> GetByIdAsync(int id);
}
