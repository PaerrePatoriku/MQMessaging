using MQCommon.Customs;

public interface IMessageConsumer
{
    public Task RegisterCallback(Func<CustomsMessage, Task> callback);
}