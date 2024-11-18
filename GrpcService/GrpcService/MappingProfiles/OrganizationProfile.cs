using AutoMapper;
using GrpcService.Entities;

namespace GrpcService.MappingProfiles;

/// <summary>
/// Used to convert Organization representation models
/// </summary>
public class OrganizationProfile : Profile
{
    /// <summary>
    /// OrganizationProfile constructor
    /// </summary>
    public OrganizationProfile() 
    {
        CreateMap<OrganizationEntity, OrganizationResponse>();
    }
}
