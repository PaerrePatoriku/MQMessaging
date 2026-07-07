using MQCommon.Customs;
using MQMessagingClient.Infra;

namespace MQMessagingClient.Services;

public class CustomsMessagingService : ICustomsMessagingService
{
    IMessageQueue _messageQueue;

    public CustomsMessagingService(IMessageQueue messageQueue)
    {
        _messageQueue = messageQueue;
    }
    public async Task HandleMessage(CustomsMessage message)
    {
        //Nothing special here, just send the message forwards to the actual queue...
        await _messageQueue.Queue(message);
    }
}