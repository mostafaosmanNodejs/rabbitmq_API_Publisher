
using MassTransit;

namespace API_Publisher.bus;

public class EventBus : IEventBus
{
    private readonly IPublishEndpoint _endpoint;

    public EventBus(IPublishEndpoint endpoint)
    {
        _endpoint = endpoint;
    }

    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : class
    {
       await _endpoint.Publish(message, cancellationToken);
    }
}
