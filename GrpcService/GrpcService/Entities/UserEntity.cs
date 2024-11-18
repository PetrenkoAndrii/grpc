namespace GrpcService.Entities;

public class UserEntity : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }

    public ICollection<UsersOrganizationsEntity> UsersOrganizations { get; set; } = new List<UsersOrganizationsEntity>();
}
