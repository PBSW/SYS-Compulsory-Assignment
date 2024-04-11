using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure;
using UserService.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer("Host=MySQL-user;database=db;user=database_user;password=Password1!;SslMode=Disabled");
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

//app.UseHttpsRedirection();


app.MapControllers().WithOpenApi();

app.Run();

