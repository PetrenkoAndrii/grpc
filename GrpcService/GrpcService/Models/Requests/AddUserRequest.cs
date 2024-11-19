namespace GrpcService.Models.Requests;

public class AddUserRequest
{
    public string? Name { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
}
