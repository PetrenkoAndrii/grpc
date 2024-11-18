using Grpc.Core;
using Grpc.Net.Client;
using HttpService.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;

namespace HttpService.Infrastructure;

public class GrpcClientFactory : IGrpcClientFactory
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly GrpcServiceConfiguration _configuration;

    public GrpcClientFactory(IHttpClientFactory httpClientFactory,
                            IOptions<GrpcServiceConfiguration> options)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = options.Value;
    }

    public T CreateClient<T>() where T : ClientBase
    {
        var channel = GrpcChannel.ForAddress(_configuration.Url!,
                        new GrpcChannelOptions { HttpClient = _httpClientFactory.CreateClient() });

        return Activator.CreateInstance(typeof(T), channel) as T
                ?? throw new InvalidOperationException($"Can not create client for type {typeof(T)}");
    }
}
