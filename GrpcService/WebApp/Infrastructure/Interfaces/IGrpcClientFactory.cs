using Grpc.Core;

namespace HttpService.Infrastructure.Interfaces;

public interface IGrpcClientFactory
{
    T CreateClient<T>() where T : ClientBase;
}
