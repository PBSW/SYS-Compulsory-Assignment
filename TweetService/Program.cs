using Microsoft.EntityFrameworkCore;
using TweetService.Infrastructure;
using TweetService.Service;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseMySql(config.GetConnectionString("TweetDatabase"),
        ServerVersion.AutoDetect(config.GetConnectionString("TweetDatabase")));
});

DependencyResolver.RegisterServices(builder.Services);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers().WithOpenApi();

app.Run();

