using GrpcService.Entities;
using Microsoft.EntityFrameworkCore;

namespace GrpcService;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<OrganizationEntity> Organizations { get; set; }
}
