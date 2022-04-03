using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

using Grpc.Core;
using Grpc.Net.Client;

using Sandbox.gRPC;

using static System.Net.Http.HttpClientHandler;

using static Grpc.Net.Client.GrpcChannel;

namespace gRPC.Client;

[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Unused Members for Demonstration Purposes")]
public class GreeterClient : Greeter.GreeterClient
{
    public GreeterClient
    (
        Uri  address
        , bool dangerousUntrustedCertificates = false
    )
        : base(BuildChannel(address.AbsolutePath, dangerousUntrustedCertificates)) { }

    public GreeterClient
    (
        string address
        , bool   dangerousUntrustedCertificates = false
    )
        : base(BuildChannel(address, dangerousUntrustedCertificates)) { }

    public GreeterClient (ChannelBase channel)
        : base(channel) { }

    public GreeterClient (CallInvoker callInvoker)
        : base(callInvoker) { }

    protected GreeterClient () { }

    protected GreeterClient (ClientBaseConfiguration configuration)
        : base(configuration) { }

    private static ChannelBase BuildChannel
    (
        string  url
        , in bool dangerousUntrustedCertificates
    )
    {
        if (!dangerousUntrustedCertificates) return ForAddress(url);

        var httpClientHandler = new HttpClientHandler
        {
            // Return `true` to allow certificates that are untrusted/invalid
            ServerCertificateCustomValidationCallback =
                DangerousAcceptAnyServerCertificateValidator
        };

        var httpClient = new HttpClient(httpClientHandler);

        return ForAddress
        (
            url
            , new GrpcChannelOptions
            {
                HttpClient = httpClient
            }
        );
    }
}