using Andres.Satrack.Test.Infrastructure;
using Andres.Satrack.Test.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SharedKernel.Infrastructure.Caching;
using SharedKernel.Infrastructure.Cqrs.Commands;
using SharedKernel.Infrastructure.Cqrs.Queries;

var builder = WebApplication.CreateBuilder(args);

//Add UserSecrets
builder.Configuration.AddUserSecrets<Program>();
string connectionString = builder.Configuration.GetConnectionString("TaskConnectionSqlServer") ?? throw new ArgumentNullException("ConnectionString", "The connection string is null");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo
{
    Title = "Andres.Satrack.Test",
    Version = "v1",
    Description = "An api test for apply in Satrack"
}));

builder.Services.AddInMemoryCommandBus();
builder.Services.AddInMemoryQueryBus();
builder.Services.AddInMemoryCache();
builder.Services.AddCore(builder.Configuration, "TaskConnectionSqlServer");
builder.Services.AddDbContext<TaskContext>(options =>
{
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TaskContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<TaskContext>>();
    try
    {
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error while migration was executing");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
