using MQCommon.Customs;
using MQMessagingClient.Services;

namespace MQMessagingClient.Routing;
/*
 * First thought of doing the classic approach of just registering controllers with attribution,
 * but newer versions of .net also support the approach of minimal APIs, that feel very at-home
 * for anyone familiar with how one would build a router for e.g express or a spa frontend.
 *
 * This class builds the (admittedly simple) routing for this application.
 */
public class IndexRouter
{
    public void Map(IEndpointRouteBuilder app)
    {
        var customs = app.MapGroup("customs");
        
        //This is a very simple API, so it might make sense to inline all the routes...
        customs.MapPost("/", async (CustomsMessage message, 
            ICustomsMessagingService service, 
            ILogger<IndexRouter> logger) =>
        {
            logger.LogInformation($"Custom message received ${message.Message}, ${message.Type}. This will be queued shortly...");
            //todo: the actual functionality...
            return Results.Ok(new
            {
                message = "Message received"
            });
        });
    }
}