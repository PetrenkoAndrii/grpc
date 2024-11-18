namespace GrpcService.Entities;

public class UserEntity : BaseEntity
{
    public string? Name { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }

    public ICollection<UsersOrganizationsEntity> UsersOrganizations { get; set; } = new List<UsersOrganizationsEntity>();
}
