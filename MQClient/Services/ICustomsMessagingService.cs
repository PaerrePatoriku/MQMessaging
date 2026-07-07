using MQCommon.Customs;

namespace MQMessagingClient.Services;

public interface ICustomsMessagingService
{
    Task HandleMessage(CustomsMessage message);
}