using GrpcClient;
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
    public async Task<IActionResult> GetOrganization(int id)
    {
        var response = await client.GetUserAsync(new GetUserRequest { Id = id });
        return Ok(response);
    }
}
