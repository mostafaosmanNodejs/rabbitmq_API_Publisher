namespace API_Publisher.bus;

public interface IEventBus
{
    Task PublishAsync<T>(T message,CancellationToken cancellationToken=default )
        where T : class;
}
