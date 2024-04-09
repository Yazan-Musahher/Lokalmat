using System.Threading.Channels;

namespace LokalProdusert.Shared.EventBus;

public class InMemoryMessageQueue
{
    private readonly Channel<IIntegrationEvent> _channel = Channel.CreateUnbounded<IIntegrationEvent>();

    public ChannelReader<IIntegrationEvent> Reader => _channel.Reader;
    public ChannelWriter<IIntegrationEvent> Writer => _channel.Writer;
}
