namespace GrpcService.Models.Requests;

public class UserOrganizationAssociationRequest
{
    public int UserId { get; set; }
    public int OrganizationId { get; set; }
}
