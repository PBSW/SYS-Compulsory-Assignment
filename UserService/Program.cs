using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using UserService.API;
using UserService.Infrastructure;
using UserService.Service;
using UserService.Service.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer("Host=postgres;Database=postgres;Username=postgres;Password=postgres");
});

//TODO
builder.Services.AddSingleton(new MessageClient(RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")));

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

