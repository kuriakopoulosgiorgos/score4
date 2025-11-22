using Application;
using GrainInterfaces.Configuration;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.FileProviders;
using score4;
using score4.Games;

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

builder.Services.AddSignalR(o =>
        o.AddFilter<ExceptionFilter>())
    .AddNewtonsoftJsonProtocol();
builder.Services.AddSingleton<GameStreamObserver>();
builder.Services.AddHostedService<GameStreamSubscriberHostService>();
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

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.WebRootPath, "src")
    ),
    RequestPath = "" // optional, serve at root URL
});

app.MapHub<GameHub>("/gameHub");

app.Run();
