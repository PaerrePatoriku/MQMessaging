using MQCommon.Customs;

namespace MQMessagingClient.Infra;


/*
 * Lets not have actual rabbit mq implement here.
 * The implementation might be for some other queueing system, so this should be DI:d like this.
 */
public interface IMessageQueue
{
    public Task Queue(CustomsMessage message);
}