namespace GrpcService.Repositories.Interfaces;

using GrpcService.Entities;

public interface IUserOrganizationRepository
{
    Task<bool> AssociateUserToOrganizationAsync(UsersOrganizationsEntity entity);
}
