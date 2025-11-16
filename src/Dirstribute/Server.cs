using System.IO.Pipes;
using StreamJsonRpc;

namespace Dirstribute;

public class Server
{
    public async Task StartNamedPipeServerAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await using var server = new NamedPipeServerStream(
                "Dirstribute",
                PipeDirection.InOut,
                NamedPipeServerStream.MaxAllowedServerInstances,
                PipeTransmissionMode.Byte,
                PipeOptions.Asynchronous
            );

            await server.WaitForConnectionAsync();

            var rpc = JsonRpc.Attach(server, new DirstributeServer());

            _ = Task.Run(async () =>
            {
                await rpc.Completion;
            });
        }
    }

    private async Task HandleClientAsync(NamedPipeServerStream server, CancellationToken token)
    {
        
    }
}
