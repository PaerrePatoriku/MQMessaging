using System.Net.Http.Json;
using System.Text.Json;
using MQCommon.Customs;

namespace ExternalMockApp;

public class MockCommunicator
{
    /*
     * In a real system the government app would have some kind of register with which the system
     * would know where to send every customs response. So a declaration would have something that identifies the company or org making the declaration,
     * then the company that sent the message (or the one that facilitates those messages) would have declared their application endpoint details to the customs system.
     *
     * For this sample, the main program simply DI:s the connection details to the communication system.
     */
    public HttpClient HttpClient { get; set; }
    public MockCommunicator(string clientURI)
    {
        HttpClient = new HttpClient(){ BaseAddress =  new Uri(clientURI)};
    }
    public List<CustomsMessage> _messages =
        new([
            //a real customs service would have a bunch more info for example, an identifier for newly registered declarations and data from the original customs message
            //Since im only showing this as an use case and only focusing on the tech stack, this is WAY more simpler here...
            new CustomsMessage("Declaration ABC1 accepted", MessageType.CUS_ACCEPTED),
            new CustomsMessage("Declaration ABC2 accepted", MessageType.CUS_ACCEPTED),
            new CustomsMessage("Declaration ABC2 missing invoice attachment!", MessageType.CUS_CUSTOMER_RESPONSE_REQUIRED),
            new CustomsMessage("Received message has invalid content [followed here by a bunch of errors or something...]", MessageType.CUS_VALIDATION_ERROR),
            new CustomsMessage("Declaration ABC1 registered.", MessageType.CUS_DECLARATION_REGISTERED),
            new CustomsMessage("Declaration ABC3 message rule violation [followed by some customs system errors here...]", MessageType.CUS_REJECTED),
        ]);
    public async Task SendMockMessage()
    {
        var random = new Random();
        var selectedIndex = random.Next(0, _messages.Count);
        var chosenMessage = _messages[selectedIndex];
        
        var messageBody = JsonSerializer.Serialize(chosenMessage);        
        
        /*
         * A touch of realism here: the contract is something that customs has declared
         * but the endpoint where customs send responses is probably just a static URL that the customer has stated, so we just send a message to that root.
         */
        
        File.WriteAllText("request.json",  messageBody);
        //lets not doubly serialize, the above is just for saving the message before sending it...
        Console.WriteLine("Sending a mock message...");
        var response = await HttpClient.PostAsJsonAsync("", chosenMessage);
        Console.WriteLine("Mock message sent! Outputting the response");
        Console.WriteLine(await response.Content.ReadAsStringAsync());

    }   
}