using MQCommon.Customs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Logging.AddConsole(); 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/customs", (CustomsMessage message, ILogger<Program> logger) =>
{
    logger.LogInformation($"Custom message received ${message.Message}, ${message.Type}. This will be queued shortly...");
});

app.Run();
