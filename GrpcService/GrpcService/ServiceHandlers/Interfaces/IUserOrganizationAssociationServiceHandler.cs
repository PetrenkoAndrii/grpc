using Model = GrpcService.Models.Requests;

namespace GrpcService.ServiceHandlers.Interfaces;

public interface IUserOrganizationAssociationServiceHandler
{
    Task<bool> AssociateUserToOrganizationAsync(Model.UserOrganizationAssociationRequest request);
    Task<bool> DisassociateUserFromOrganizationAsync(Model.UserOrganizationAssociationRequest request);
}
