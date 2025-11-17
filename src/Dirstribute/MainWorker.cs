using System.Collections.Concurrent;
using Dirstribute.Helpers;

namespace Dirstribute;

public class MainWorker : BackgroundService
{
    public readonly BlockingCollection<Action> Queue = new();
    private readonly ILogger<MainWorker> _logger;

    public MainWorker(ILogger<MainWorker> logger, ConfigurationService configurationService)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var action = Queue.Take(stoppingToken);
            action();
        }
    }

    public void Run(Action action)
    {
        Queue.Add(action);
    }

    public Task<T?> RunAsync<T>(Func<T?> action)
    {
        var tsc = new TaskCompletionSource<T?>();
        
        Queue.Add(() => tsc.SetResult(action()));

        return tsc.Task;
    }
}
