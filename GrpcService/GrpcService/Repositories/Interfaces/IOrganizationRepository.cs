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

    /// <summary>
    /// Sets IsDeleted Organization field to 'true'.
    /// </summary>
    /// <param name="Organization">Organization to process</param>
    /// <returns>True - if entity was updated, False - if it wasn't</returns>
    Task<bool> DeleteAsync(OrganizationEntity organization);

    /// <summary>
    /// Returns is Organization exist via all organizations
    /// </summary>
    /// <param name="id">Organization's id</param>
    /// <returns>True - if Organization exist, False - if it isn't</returns>
    Task<bool> IsOrganizationExistAsync(int id);
}
