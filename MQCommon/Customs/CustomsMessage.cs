namespace MQCommon.Customs;

public class CustomsMessage
{
    public MessageType Type { get; set; }
    public string Message { get; set; } = ""; //For simplicitys' sake the message is just a string with the message type....
    public CustomsMessage(string message, MessageType type)
    {
        Message = message;
        Type = type;
    }
}