using GrpcService.Entities;
using GrpcService.Repositories.Interfaces;
using GrpcService.ServiceHandlers.Interfaces;
using Model = GrpcService.Models.Requests;

namespace GrpcService.ServiceHandlers;

public class UserOrganizationAssociationServiceHandler : IUserOrganizationAssociationServiceHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IUserOrganizationRepository _userOrganizationRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserOrganizationAssociationServiceHandler"/> class.
    /// </summary>
    /// <param name="userRepository">User repository <see cref="IUserRepository"/> class</param>
    /// <param name="organizationRepository">Organization repository <see cref="IOrganizationRepository"/> class</param>
    /// <param name="userOrganizationRepository">UserOrganization repository <see cref="IUserOrganizationRepository"/> class</param>
    public UserOrganizationAssociationServiceHandler(
                        IUserRepository userRepository,
                        IOrganizationRepository organizationRepository,
                        IUserOrganizationRepository userOrganizationRepository)
    {
        _userRepository = userRepository;
        _organizationRepository = organizationRepository;
        _userOrganizationRepository = userOrganizationRepository;
    }

    public async Task<bool> AssociateUserToOrganizationAsync(Model.UserOrganizationAssociationRequest request)
    {
        var isUserExist = await _userRepository.IsUserExistAsync(request.UserId);
        if (!isUserExist)
            return false;

        var isOrganizationExist = await _organizationRepository.IsOrganizationExistAsync(request.OrganizationId);
        if (!isOrganizationExist)
            return false;

        var entity = new UsersOrganizationsEntity(request.UserId, request.OrganizationId);
        
        var isSuccess = await _userOrganizationRepository.AssociateUserToOrganizationAsync(entity);
        return isSuccess;
    }

    public async Task<bool> DisassociateUserFromOrganizationAsync(Model.UserOrganizationAssociationRequest request)
    {
        var isSuccess = await _userOrganizationRepository.DisassociateUserFromOrganizationAsync(request.UserId, request.OrganizationId);
        return isSuccess;
    }
}
