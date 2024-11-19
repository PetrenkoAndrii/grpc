using AutoMapper;
using GrpcService.Entities;
using GrpcService.Models.Responses;
using GrpcService.Repositories.Interfaces;
using GrpcService.ServiceHandlers.Interfaces;
using Model = GrpcService.Models.Requests;

namespace GrpcService.ServiceHandlers;

public class UserServiceHandler : IUserServiceHandler
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserServiceHandler"/> class.
    /// </summary>
    /// <param name="repository">Repository</param>
    /// <param name="mapper">The mapper.</param>
    public UserServiceHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> AddUserAsync(Model.AddUserRequest request)
    {
        bool isUniqueUserName = await _repository.IsUniqueUserNameAsync(request.UserName!);
        if (!isUniqueUserName)
            return -1;

        bool isUniqueEmail = await _repository.IsUniqueEmailAsync(request.Email!);
        if (!isUniqueEmail)
            return -1;

        var dateTime = DateTime.UtcNow;
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
        return addedEntityId;
    }

    public async Task<GetUserResponse> GetUserAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);

        var response = _mapper.Map<GetUserResponse>(user);
        return response;
    }
}
