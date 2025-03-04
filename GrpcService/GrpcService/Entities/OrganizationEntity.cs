﻿namespace GrpcService.Entities;

public class OrganizationEntity : BaseEntity
{
    public string? Name { get; set; }
    public string? Address { get; set; }

    public ICollection<UsersOrganizationsEntity> UsersOrganizations { get; set; } = new List<UsersOrganizationsEntity>();
}
