using GrpcService.Entities;
using Microsoft.EntityFrameworkCore;

namespace GrpcService;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<OrganizationEntity> Organizations { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UsersOrganizationsEntity> UsersOrganizations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UsersOrganizationsEntity>()
            .HasKey(uo => new { uo.UserId, uo.OrganizationId });

        modelBuilder.Entity<UsersOrganizationsEntity>()
            .HasOne(sc => sc.User)
            .WithMany(s => s.UsersOrganizations)
            .HasForeignKey(sc => sc.UserId);

        modelBuilder.Entity<UsersOrganizationsEntity>()
            .HasOne(sc => sc.Organization)
            .WithMany(s => s.UsersOrganizations)
            .HasForeignKey(sc => sc.OrganizationId);
    }
}
