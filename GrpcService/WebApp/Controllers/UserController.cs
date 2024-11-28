using GrpcClient;
using HttpService.Extensions;
using HttpService.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static GrpcClient.User;

namespace HttpService.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserClient client;
    private readonly IGrpcClientFactory _clientFactory;

    public UserController(IGrpcClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        client = _clientFactory.CreateClient<UserClient>();
    }

    [HttpGet]
    public async Task<IActionResult> GetUser(int id)
    {
        var response = await client.GetUserAsync(new GetUserRequest { Id = id });
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(string name, string userName, string email)
    {
        bool isValid = email.ValidateEmail();
        if (!isValid)
            return BadRequest("Not valid email");
        
        var request = new AddUserRequest
        {
            Name = name,
            UserName = userName,
            Email = email
        };

        var response = await client.AddUserAsync(request);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> AssociateUserToOrganization(int userId, int organizationId)
    {
        var request = new UserOrganizationAssociationRequest
        {
            UserId = userId,
            OrganizationId = organizationId
        };

        var response = await client.AssociateUserToOrganizationAsync(request);
        return Ok(response);
    }
}
