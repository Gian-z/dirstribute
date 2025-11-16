using System.IO.Pipes;
using StreamJsonRpc;

namespace Dirstribute;

public class NamedPipeServer : BackgroundService
{
    private readonly ILogger _logger;
    
    public NamedPipeServer(ILogger<NamedPipeServer> logger)
    {
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await using var server = new NamedPipeServerStream(
                nameof(DirstributeServer),
                PipeDirection.InOut,
                NamedPipeServerStream.MaxAllowedServerInstances,
                PipeTransmissionMode.Byte,
                PipeOptions.Asynchronous
            );

            await server.WaitForConnectionAsync(stoppingToken);

            var rpc = JsonRpc.Attach(server, new DirstributeServer());

            _ = Task.Run(async () => { await rpc.Completion; }, stoppingToken);
        }
    }
}
