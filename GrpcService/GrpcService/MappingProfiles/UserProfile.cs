using AutoMapper;
using GrpcService.Entities;
using GrpcService.Models.Responses;
using Model = GrpcService.Models.Requests;

namespace GrpcService.MappingProfiles;

public class UserProfile : Profile
{
    /// <summary>
    /// UserProfile constructor
    /// </summary>
    public UserProfile()
    {
        CreateMap<AddUserRequest, Model.AddUserRequest>();

        CreateMap<UserEntity, GetUserResponse>();
        CreateMap<GetUserResponse, UserResponse>()
            .ForMember(dest => dest.CreatedAt, option => option.MapFrom(s => s.CreatedAt.ToString("O")))
            .ForMember(dest => dest.UpdatedAt, option => option.MapFrom(s => s.UpdatedAt.ToString("O")));

        CreateMap<UserOrganizationAssociationRequest, Model.UserOrganizationAssociationRequest>();
    }
}
