using LokalProdusert.Shared.Outbox;
using MediatR;
using MongoDB.Driver;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Quartz;

namespace LokalProdusert.Shared.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly IPublisher _publisher;
    private readonly IMongoCollection<OutboxMessage> _outboxCollection;
    
    public ProcessOutboxMessagesJob(
     IPublisher publisher,
     IMongoDatabase database
     )
    {
        _publisher = publisher;
        _outboxCollection = database.GetCollection<OutboxMessage>("OutboxMessages");
    }

    public async Task Execute(IJobExecutionContext context)
    {
        List<OutboxMessage> messages = await _outboxCollection
            .Find(x => x.ProcessedOn == null)
            .Limit(20)
            .ToListAsync(context.CancellationToken);

        foreach (OutboxMessage outboxMessage in messages)
        {
            try
            {
                var type = Type.GetType(outboxMessage.MessageType);
                if (type == null)
                {
                    continue;
                }

                var domainEvent = JsonConvert.DeserializeObject(outboxMessage.Message, type,
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

                if (domainEvent == null)
                {
                    continue;
                }

                AsyncRetryPolicy retryPolicy = Policy
                    .Handle<Exception>()
                    .RetryAsync(3);

                await retryPolicy.ExecuteAsync(async () =>
                {
                    await _publisher.Publish(domainEvent, context.CancellationToken);
                    outboxMessage.ProcessedOn = DateTime.UtcNow;
                    outboxMessage.Error = null;
                });
            }
            catch (JsonReaderException ex)
            {
                outboxMessage.Error = ex.ToString();
            }
            finally
            {
                var filter = Builders<OutboxMessage>.Filter.Eq(m => m.Id, outboxMessage.Id);
                var update = Builders<OutboxMessage>.Update
                    .Set(m => m.ProcessedOn, outboxMessage.ProcessedOn)
                    .Set(m => m.Error, outboxMessage.Error);

                await _outboxCollection.UpdateOneAsync(filter, update, cancellationToken: context.CancellationToken);
            }
        }

    }

}