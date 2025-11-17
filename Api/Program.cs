using Application;
using GrainInterfaces.Configuration;
using score4;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseOrleans(siloBuilder =>
    {
        siloBuilder.UseLocalhostClustering();
        siloBuilder.AddMemoryStreams(Streams.GameStream);
        siloBuilder.AddMemoryGrainStorage("PubSubStore");
    });


// Add services to the container.
builder.Services
    .AddApplication()
    .AddControllers();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

app.Run();
