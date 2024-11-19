using GrpcService.Models.Responses;
using Model = GrpcService.Models.Requests;

namespace GrpcService.ServiceHandlers.Interfaces;

public interface IUserServiceHandler
{
    Task<GetUserResponse> GetUserAsync(int id);
    Task<int> AddUserAsync(Model.AddUserRequest request);
}
