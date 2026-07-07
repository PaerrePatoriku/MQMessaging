using MQCommon.Infra;
using MQMessagingWorkerApp;
using MQMessagingWorkerApp.Consumer;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.Configure<RabbitMQOptions>(builder.Configuration.GetSection("RabbitMQOptions"));
builder.Services.AddSingleton(typeof(ICustomsMQQueue), typeof(CustomsMQQueue));
builder.Services.AddSingleton(typeof(IMessageConsumer), typeof(MQMessageConsumer));
builder.Services.AddHostedService<Worker>();
var host = builder.Build();
host.Run();