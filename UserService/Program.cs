using EasyNetQ;
using UserService.API;
using UserService.Service;
using UserService.Service.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//TODO
var bus = RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest");
builder.Services.AddSingleton(bus);
builder.Services.AddHostedService<MessageHandler>();

DependencyResolver.RegisterServices(builder.Services);

builder.Services.AddControllers();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();


app.MapControllers().WithOpenApi();

app.Run();

