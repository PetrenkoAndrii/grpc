using AutoMapper;
using Grpc.Core;
using GrpcService.Entities;
using GrpcService.Repositories.Interfaces;

namespace GrpcService.Services;

public class OrganizationService : Organization.OrganizationBase
{
    private readonly IOrganizationRepository _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrganizationService"/> class.
    /// </summary>
    /// <param name="repository">Repository</param>
    /// <param name="mapper">The mapper.</param>
    public OrganizationService(IOrganizationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Service for AddOrganization
    /// </summary>
    /// <param name="request">Holds AddOrganizationRequest parameters</param>
    /// <param name="context">ServerCallContext context</param>
    /// <returns>Organization item response</returns>
    public override async Task<AddOrganizationResponse> AddOrganization(AddOrganizationRequest request, ServerCallContext context)
    {
        var isUniqueName = await _repository.IsUniqueNameAsync(request.Name);
        if (!isUniqueName)
            return new AddOrganizationResponse { Id = -1 };

        var dateTime = DateTime.Now;
        var entity = new OrganizationEntity
        {
            Address = request.Address,
            Name = request.Name,
            IsDeleted = false,
            CreatedAt = dateTime,
            UpdatedAt = dateTime,
        };

        var addedEntityId = await _repository.AddAsync(entity);
        return new AddOrganizationResponse { Id = addedEntityId };
    }

    /// <summary>
    /// Service for GetOrganization
    /// </summary>
    /// <param name="request">Holds GetOrganizationRequest parameters</param>
    /// <param name="context">ServerCallContext context</param>
    /// <returns>Single Organization item response</returns>
    public override async Task<OrganizationResponse> GetOrganization(GetOrganizationRequest request, ServerCallContext context)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity == null)
            return new OrganizationResponse { Name = string.Empty, Address = string.Empty };

        var response = _mapper.Map<OrganizationResponse>(entity);
        return response;
    }

    /// <summary>
    /// Service for DeleteOrganization
    /// </summary>
    /// <param name="request">Holds DeleteOrganizationRequest parameters</param>
    /// <param name="context">ServerCallContext context</param>
    /// <returns>DeleteOrganizationResponse response which contains IsSuccess field</returns>
    public override async Task<DeleteOrganizationResponse> DeleteOrganization(DeleteOrganizationRequest request, ServerCallContext context)
    {
        var response = new DeleteOrganizationResponse();
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity == null)
        {
            response.IsSuccess = false;
            return response;
        }

        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.Now;
        var isSuccess = await _repository.DeleteAsync(entity);

        response.IsSuccess = isSuccess;
        return response;
    }
}
