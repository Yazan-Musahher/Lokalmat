using MediatR;
namespace LokalProdusert.Shared.EventBus;

using Quartz;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


public class ProcessIntegrationEventsJob : IJob
{
    private readonly InMemoryMessageQueue _queue;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<ProcessIntegrationEventsJob> _logger;

    public ProcessIntegrationEventsJob(InMemoryMessageQueue queue, 
            IServiceScopeFactory serviceScopeFactory, 
            ILogger<ProcessIntegrationEventsJob> logger)
    {
        _queue = queue;
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        if (context.Recovering)
        {
            _logger.LogInformation("Job is recovering from a previous scheduler failure. Recovering Trigger Key: {RecoveringTriggerKey}", context.RecoveringTriggerKey);
        }
        while (_queue.Reader.TryRead(out var integrationEvent))
        {
            try
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

                await publisher.Publish(integrationEvent, context.CancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong! {IntegrationEventId}", integrationEvent);
            }
        }
    }
}
