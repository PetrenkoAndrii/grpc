namespace GrpcService.Models.Responses;

public class GetUserResponse : BaseResponse
{
    public string? Name { get; set; }
    public string? UserName { get;set; }
    public string? Email { get; set; }
}
