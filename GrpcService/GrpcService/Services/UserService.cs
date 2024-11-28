using AutoMapper;
using Grpc.Core;
using GrpcService.ServiceHandlers.Interfaces;
using static GrpcService.User;
using Model = GrpcService.Models.Requests;

namespace GrpcService.Services;

public class UserService : UserBase
{
    private readonly IUserServiceHandler _userServiceHandler;
    private readonly IUserOrganizationAssociationServiceHandler _userOrganizationAssociationServiceHandler;
    private readonly IMapper _mapper;

    public UserService(IUserServiceHandler userServiceHandler,
        IUserOrganizationAssociationServiceHandler userOrganizationAssociationServiceHandler,
        IMapper mapper)
    {
        _userServiceHandler = userServiceHandler;
        _userOrganizationAssociationServiceHandler = userOrganizationAssociationServiceHandler;
        _mapper = mapper;
    }

    /// <summary>
    /// Service for GetUser
    /// </summary>
    /// <param name="request">Holds GetUserRequest parameters</param>
    /// <param name="context">ServerCallContext context</param>
    /// <returns>Single User item response</returns>
    public async override Task<UserResponse> GetUser(GetUserRequest request, ServerCallContext context)
    {
        var userResponse = await _userServiceHandler.GetUserAsync(request.Id);
        if (userResponse == null)
        {
            return new UserResponse
            {
                Name = string.Empty,
                Username = string.Empty,
                Email = string.Empty,
                CreatedAt = string.Empty,
                UpdatedAt = string.Empty,
            };
        }

        var response = _mapper.Map<UserResponse>(userResponse);
        return response;
    }

    /// <summary>
    /// Service for AddUser
    /// </summary>
    /// <param name="request">Holds AddUserRequest parameters</param>
    /// <param name="context">ServerCallContext context</param>
    /// <returns>Id of created user</returns>
    public async override Task<AddUserResponse> AddUser(AddUserRequest request, ServerCallContext context)
    {
        var modelRequest = _mapper.Map<Model.AddUserRequest>(request);
        var userId = await _userServiceHandler.AddUserAsync(modelRequest);

        var response = new AddUserResponse { Id = userId };
        return response;
    }

    /// <summary>
    /// Service for AssociateUserToOrganization
    /// </summary>
    /// <param name="request">Holds UserOrganizationAssociationRequest parameters</param>
    /// <param name="context">ServerCallContext context</param>
    /// <returns>UserOrganizationAssociationResponse response which contains IsSuccess field</returns>
    public async override Task<UserOrganizationAssociationResponse> AssociateUserToOrganization(UserOrganizationAssociationRequest request, ServerCallContext context)
    {
        var modelRequest = _mapper.Map<Model.UserOrganizationAssociationRequest>(request);
        var isSuccessfullAssociated = await _userOrganizationAssociationServiceHandler.AssociateUserToOrganizationAsync(modelRequest);

        var response = new UserOrganizationAssociationResponse { IsSuccess = isSuccessfullAssociated };
        return response;
    }
}
