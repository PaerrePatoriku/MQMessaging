using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace MQCommon.Infra;

public class CustomsMQQueue : ICustomsMQQueue
{
    private IConnection _connection;
    private IChannel _channel;
    private RabbitMQOptions _options;

    public IChannel GetChannel()
    {
        return _channel;
    }
    public CustomsMQQueue(IOptions<RabbitMQOptions> options)
    {
        _options = options.Value;
        Initialize().Wait();
    }
    async Task Initialize()
    {
        var fac = new ConnectionFactory { HostName = _options.HostName};
        
        _connection = await fac.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        /*
         * More about channel creation options.
         * queue declares the channel name (identifier)
         * durable = true means that restarting the broker doesnt delete the queue, so we dont lose messages in the queue
         * exclusive = false means that the queue is not limited to the connection that declared it. The actual app in this also uses the same queue, so this should be false
         * The external simulated connection DOES NOT use this connection. There is an abstraction between the "customs system" and the app that uses the queue for message handling
         * autoDelete = false, means that the queue stays alive even if all connections are closed.
         *
         */
        await _channel.QueueDeclareAsync(
            queue: _options.CustomsQueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
    }
}

public interface ICustomsMQQueue
{
    public IChannel GetChannel();
}