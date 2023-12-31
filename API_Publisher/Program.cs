using API_Publisher;
using API_Publisher.bus;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Message Broker Settings Configuration 
builder.Services.Configure<MessageBrokerSettings>(
    builder.Configuration.GetSection("MessageBroker"));
builder.Services.AddSingleton(sp=>
sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

// MassTransit Configuration
builder.Services.AddMassTransit(busConfigure =>
{
    busConfigure.SetKebabCaseEndpointNameFormatter();
    busConfigure.UsingRabbitMq((context, configurator) =>
    {
        MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();
        configurator.Host(new Uri(settings.Host), h =>
        {
            h.Username(settings.UserName);
            h.Password(settings.Password);
        });
    });
});
// EventBus Regstration
builder.Services.AddTransient<IEventBus, EventBus>();  
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
