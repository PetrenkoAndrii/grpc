namespace GrpcService.Entities;

public class UsersOrganizationsEntity : BaseEntity
{
    public int UserId { get; set; }
    public UserEntity? User { get; set; }

    public int OrganizationId { get; set; }
    public OrganizationEntity? Organization { get; set; }

    public UsersOrganizationsEntity()
    { }

    public UsersOrganizationsEntity(int usertId, int organizationId)
    {
        var dateOfExecution = DateTime.UtcNow;
        UserId = usertId;
        OrganizationId = organizationId;
        CreatedAt = dateOfExecution;
        UpdatedAt = dateOfExecution;
    }
}
