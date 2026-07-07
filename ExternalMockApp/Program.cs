namespace ExternalMockApp;


/*
 This is a simple program that mocks how an external service would send messages to the 
 API, that then queues messages to the main worker.
 
 Simply run the client, then press enter to send a mock "customs message"
 This simulates a message that is similar in content to what a government customs system would send
 as a response to e.g a customs declaration, after the message was automatically or manually handled by
 customs workers.
 */
class Program
{
    static void Main(string[] args)
    {
        //We could .env this info later on.
        MockCommunicator c = new MockCommunicator("http://localhost:5017/customs");
        while (true)
        {
            Console.WriteLine("Press any key to send a mock message... (CTRL + C to end)");
            Console.ReadLine();
            c.SendMockMessage().Wait(); //the io looks silly if we dont await first. This is ok for a console app like this...
        }
    }
}