using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using MQCommon.Customs;
using MQCommon.Infra;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MQMessagingWorkerApp.Consumer;

public class MQMessageConsumer : IMessageConsumer
{
    private IChannel _channel;
    private RabbitMQOptions _options;

    public MQMessageConsumer(IOptions<RabbitMQOptions> options, ICustomsMQQueue customsQueue)
    {
        _options = options.Value;
        _channel = customsQueue.GetChannel();
    }

    public async Task RegisterCallback(Func<CustomsMessage, Task> callback)
    {
        
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (_, eventArgs) =>
        {
            var _json = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            var _customsMessage = JsonSerializer.Deserialize<CustomsMessage>(_json);

            if (_customsMessage != null)
            {
                await callback(_customsMessage);
            }
            
            //Multiple = false means that this does not ack all messages, just this one.
            await _channel.BasicAckAsync(eventArgs.DeliveryTag, false);
        };
        await _channel.BasicConsumeAsync(_options.CustomsQueueName, false, consumer);
    }
    
}

