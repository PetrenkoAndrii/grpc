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
}
