﻿using GrpcClient;
using HttpService.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static GrpcClient.Organization;

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

    [HttpPost]
    public async Task<IActionResult> GetOrganization(string name, string address)
    {
        var response = await client.AddOrganizationAsync(new AddOrganizationRequest { Name = name, Address = address });
        return Ok(response);
    }
}
