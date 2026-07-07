using System.Text;
using System.Text.Json;
using MQCommon.Customs;
using RabbitMQ.Client;

namespace MQMessagingClient.Infra;

public class RabbitMqQueue : IMessageQueue
{
    private IConnection _connection;
    private IChannel _channel;
    public RabbitMqQueue()
    {
        Initialize().Wait();
        Console.WriteLine("MQ Infra is ready!");
    }

    async Task Initialize()
    {
        var fac = new ConnectionFactory { HostName = "localhost" };
        
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
            queue: "customs",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
    }

    public async Task Queue(CustomsMessage message)
    {
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        await _channel.BasicPublishAsync(exchange: "", routingKey: "customs", body: body);
    }
    
}