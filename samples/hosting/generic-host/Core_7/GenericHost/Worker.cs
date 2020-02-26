using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus;

internal class Worker : BackgroundService
{
    private readonly IServiceProvider provider;

    public Worker(IServiceProvider provider)
    {
        this.provider = provider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            // we can only retrieve it here because the job host is started before NServiceBus is started
            var session = provider.GetService<IMessageSession>();

            var number = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                // sending here to simulate work
                await session.SendLocal(new MyMessage {Number = number++})
                    .ConfigureAwait(false);

                await Task.Delay(1000, stoppingToken).ConfigureAwait(false);
            }
        }
        catch (OperationCanceledException)
        {
            // graceful shutdown
        }
    }
}