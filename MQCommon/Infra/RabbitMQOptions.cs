namespace MQCommon.Infra;

public class RabbitMQOptions
{
    /*
     * The idea is to share common props between the broker and the app itself.
     * 
     */
    public string HostName { get; set; } = "localhost";

    public string CustomsQueueName { get; set; } = "customs";

}