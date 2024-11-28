using GrpcService;
using HttpService.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static GrpcService.Organization;

namespace HttpService.Controllers;

[ApiController]
[Route("[controller]")]
public class OrganizationController : ControllerBase
{
    private readonly OrganizationClient client;
    private readonly IGrpcClientFactory _clientFactory;
    
    public OrganizationController(IGrpcClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        client = _clientFactory.CreateClient<OrganizationClient>();
    }

    [HttpGet]
    public async Task<IActionResult> GetOrganization(int id)
    {
        var response = await client.GetOrganizationAsync(new GetOrganizationRequest { Id = id });
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrganization(string name, string address)
    {
        var response = await client.AddOrganizationAsync(new AddOrganizationRequest { Name = name, Address = address });
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteOrganization(int id)
    {
        var response = await client.DeleteOrganizationAsync(new DeleteOrganizationRequest { Id = id }); 
        return Ok(response);
    }
}
