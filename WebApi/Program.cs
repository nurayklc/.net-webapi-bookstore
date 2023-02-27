using System.Threading;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using System.Reflection;
using WebApi.Middlewares;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext(options => options.UseInMemoryDatabase(databaseName:"BookStoreDB"));
builder.Services.AddDbContext<BookStoreDbContext>(opt=>opt.UseInMemoryDatabase(databaseName: "BookStoreDB"));
builder.Services.AddScoped<IBookStoreDbContext>(provider => provider.GetService<BookStoreDbContext>());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); 

builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();
builder.Services.AddSingleton<ILoggerService, DBLogger>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope()) 
{ 
    var services = scope.ServiceProvider; 
    DataGenerator.Initialize(services); 
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionMiddle();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
