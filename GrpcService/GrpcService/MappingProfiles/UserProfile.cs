using AutoMapper;
using GrpcService.Entities;

namespace GrpcService.MappingProfiles;

public class UserProfile : Profile
{
    /// <summary>
    /// UserProfile constructor
    /// </summary>
    public UserProfile()
    {
        CreateMap<UserEntity, UserResponse>()
            .ForMember(dest => dest.CreatedAt, option => option.MapFrom(s => s.CreatedAt.ToString("O")))
            .ForMember(dest => dest.UpdatedAt, option => option.MapFrom(s => s.UpdatedAt.ToString("O")));
    }
}
