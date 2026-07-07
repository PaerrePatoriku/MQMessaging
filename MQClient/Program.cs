using MQCommon.Customs;
using MQCommon.Infra;
using MQMessagingClient.Infra;
using MQMessagingClient.Routing;
using MQMessagingClient.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole(); 
builder.Services.AddOpenApi();

builder.Services.Configure<RabbitMQOptions>(builder.Configuration.GetSection("RabbitMQOptions"));
builder.Services.AddSingleton(typeof(ICustomsMQQueue), typeof(CustomsMQQueue));
builder.Services.AddSingleton<IMessageQueue, RabbitMqQueue>();
builder.Services.AddScoped(typeof(ICustomsMessagingService), typeof(CustomsMessagingService));

var app = builder.Build();

//Lets leverage minimal API to build a non attribute based simple API.
//I'm usually used to using the attribution system, but since I also want to learn this,
//(it sounds familiar to express, or SPA router routing, so I like it) I will also
//implement a minimal API.
var index = new IndexRouter();
index.Map(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();
