using AutoMapper;
using Grpc.Core;
using GrpcService.Entities;
using GrpcService.Repositories.Interfaces;
using static GrpcService.User;

namespace GrpcService.Services;

public class UserService : UserBase
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    /// <param name="repository">Repository</param>
    /// <param name="mapper">The mapper.</param>
    public UserService(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
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
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity == null)
            return new UserResponse 
            { 
                Name = string.Empty, 
                Username = string.Empty, 
                Email = string.Empty,
                CreatedAt = string.Empty,
                UpdatedAt = string.Empty,
            };

        var response = _mapper.Map<UserResponse>(entity);
        return response;
    }

    public async override Task<AddUserResponse> AddUser(AddUserRequest request, ServerCallContext context)
    {
        var isUniqueUserName = await _repository.IsUniqueUserNameAsync(request.UserName);
        if (!isUniqueUserName)
            return new AddUserResponse { Id = -1 };

        var isUniqueEmail = await _repository.IsUniqueEmailAsync(request.Email);
        if (!isUniqueEmail)
            return new AddUserResponse { Id = -1 };

        var dateTime = DateTime.Now;
        var entity = new UserEntity
        {
            Name = request.Name,
            UserName = request.UserName,
            Email = request.Email,
            IsDeleted = false,
            CreatedAt = dateTime,
            UpdatedAt = dateTime
        };

        var addedEntityId = await _repository.AddAsync(entity);
        return new AddUserResponse { Id = addedEntityId };
    }
}
