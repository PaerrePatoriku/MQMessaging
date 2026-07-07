using MQMessagingWorkerApp.Consumer;

namespace MQMessagingWorkerApp;

public class Worker : BackgroundService
{
    
    IMessageConsumer _consumer;
    public Worker(IMessageConsumer messageConsumer)
    {
        _consumer = messageConsumer;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        /*
         * In a more advanced system this would be more complex e.g this could new up or DI some other service that
         * then does a bunch of other stuff using the customs response message.
         * However the main thing here demonstrated is that the callback works + this worker does not refer to
         * the inner workings of rabbitMQ. It only DIs the necessary consumer class and registers its own inline function to it.
         */
        await _consumer.RegisterCallback(async customsMessage =>
        {
            Console.WriteLine("A message was received through the MQ queue!");
            Console.WriteLine($"{customsMessage.Type} -- ${customsMessage.Message}");
        });

    }
}