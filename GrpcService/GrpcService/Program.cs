using GrpcService;
using GrpcService.Entities;
using GrpcService.Repositories;
using GrpcService.Repositories.Interfaces;
using GrpcService.ServiceHandlers;
using GrpcService.ServiceHandlers.Interfaces;
using GrpcService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemoryDB"));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserOrganizationRepository, UserOrganizationRepository>();
builder.Services.AddScoped<IUserServiceHandler, UserServiceHandler>();
builder.Services.AddScoped<IUserOrganizationAssociationServiceHandler, UserOrganizationAssociationServiceHandler>();

var app = builder.Build();

await SeedDatabaseAsync(app.Services);

// Configure the HTTP request pipeline.
app.MapGrpcService<OrganizationService>();
app.MapGrpcService<UserService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

static async Task SeedDatabaseAsync(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Organizations.Any())
    {
        int addressCounter = 1;
        for (int i = 1; i < 21; i++)
        {
            bool isDeleted = false;
            DateTime? deletedAt = null;
            if (i % 2 == 0)
            {
                isDeleted = true;
                deletedAt = DateTime.Now.AddDays(-i);
            }

            await context.Organizations.AddAsync(new OrganizationEntity
            {
                Id = i,
                Name = $"Organization_{i}",
                Address = $"Address{addressCounter}",
                IsDeleted = isDeleted,
                DeletedAt = deletedAt,
                CreatedAt = DateTime.Now.AddDays(-(i + 20)),
                UpdatedAt = DateTime.Now.AddDays(-(i + 15))
            });
            addressCounter++;
        }

        await context.SaveChangesAsync();
    }

    if (!context.Users.Any())
    {
        for (int i = 1; i <= 10; i++)
        {
            await context.Users.AddAsync(new UserEntity
            {
                Id = i,
                Email = $"test{i}@gmail.com",
                Name = $"Name {i}",
                UserName = $"User {i}",
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
        }

        await context.SaveChangesAsync();
    }

    if (!context.UsersOrganizations.Any()) 
    {
        await context.UsersOrganizations.AddRangeAsync(
            new UsersOrganizationsEntity { Id = 1, UserId = 3, OrganizationId = 1 },
            new UsersOrganizationsEntity { Id = 2, UserId = 1, OrganizationId = 5 },
            new UsersOrganizationsEntity { Id = 3, UserId = 2, OrganizationId = 5 },
            new UsersOrganizationsEntity { Id = 4, UserId = 2, OrganizationId = 1 },
            new UsersOrganizationsEntity { Id = 5, UserId = 7, OrganizationId = 3 },
            new UsersOrganizationsEntity { Id = 6, UserId = 7, OrganizationId = 2 },
            new UsersOrganizationsEntity { Id = 7, UserId = 5, OrganizationId = 4 }
        );

        await context.SaveChangesAsync();
    }
}
