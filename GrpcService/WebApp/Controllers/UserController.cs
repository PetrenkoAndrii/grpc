using GrpcService;
using HttpService.Extensions;
using HttpService.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static GrpcService.User;

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

    [HttpPost("add-user")]
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

    [HttpPost("associate-user-to-organization")]
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

    [HttpPut("disassociate-user-from-organization")]
    public async Task<IActionResult> DosassociateUserFromOrganization(int userId, int organizationId)
    {
        var request = new UserOrganizationAssociationRequest
        {
            UserId = userId,
            OrganizationId = organizationId
        };

        var response = await client.DisassociateUserFromOrganizationAsync(request);
        return Ok(response);
    }
}
