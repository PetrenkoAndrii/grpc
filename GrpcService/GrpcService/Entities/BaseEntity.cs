using System.Diagnostics.CodeAnalysis;

namespace GrpcService.Entities;

/// <summary>
/// The base class for all database entities.
/// </summary>
[ExcludeFromCodeCoverage]
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
