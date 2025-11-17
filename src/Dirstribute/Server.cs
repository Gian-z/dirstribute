using System.IO.Pipes;
using Dirstribute.Helpers;
using StreamJsonRpc;

namespace Dirstribute;

public class NamedPipeServer : BackgroundService
{
    private readonly ILogger _logger;
    private readonly ConfigurationService _configurationService;
    private readonly DirstributeServer _server;

    public NamedPipeServer(ILogger<NamedPipeServer> logger, ConfigurationService configurationService, DirstributeServer server)
    {
        _logger = logger;
        _configurationService = configurationService;
        _server = server;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await using var server = new NamedPipeServerStream(
                nameof(MainWorker),
                PipeDirection.InOut,
                NamedPipeServerStream.MaxAllowedServerInstances,
                PipeTransmissionMode.Byte,
                PipeOptions.Asynchronous
            );

            await server.WaitForConnectionAsync(stoppingToken);

            var rpc = JsonRpc.Attach(server, _server);
            
            await rpc.Completion;
        }
    }
}
