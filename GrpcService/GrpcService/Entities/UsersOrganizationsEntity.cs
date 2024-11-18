namespace GrpcService.Entities;

public class UsersOrganizationsEntity : BaseEntity
{
    public int UserId { get; set; }
    public UserEntity? User { get; set; }

    public int OrganizationId { get; set; }
    public OrganizationEntity? Organization { get; set; }
}
