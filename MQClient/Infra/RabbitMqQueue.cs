using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using MQCommon.Customs;
using MQCommon.Infra;
using RabbitMQ.Client;

namespace MQMessagingClient.Infra;

public class RabbitMqQueue : IMessageQueue
{
    private IChannel _channel;
    private RabbitMQOptions _options;
    public RabbitMqQueue(IOptions<RabbitMQOptions> options, ICustomsMQQueue customsQueue)
    {
        /*
         * The setup is not that this class itself instantiates the queue.
         * Instead, the queue program is shared but initialized twice. Once in the app, and once in the
         * API for the message broker.
         *
         * The message broker accepts customs messages
         * The app reads these customs messages.
        */
        _options = options.Value;
        _channel = customsQueue.GetChannel();
        Console.WriteLine("MQ Infra is ready!");
    }
    

    public async Task Queue(CustomsMessage message)
    {
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        await _channel.BasicPublishAsync(exchange: "", routingKey: _options.CustomsQueueName, mandatory: true, basicProperties: new BasicProperties { Persistent = true }, body: body);
    }
    
}