using GrainInterfaces.Configuration;
using GrainInterfaces.Games;
using Microsoft.Extensions.Hosting;

namespace Application.Games;

public class GameStreamSubscriberHostService(
    IClusterClient clusterClient,
    GameStreamObserver gameStreamObserver
) : IHostedService
{
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var streamProvider = clusterClient.GetStreamProvider(Streams.GameStream);
        var streamId = StreamId.Create(Streams.GameStreamNameSpace, Streams.GameStreamId);
        var stream = streamProvider.GetStream<GameUpdateDto>(streamId);
        await stream.SubscribeAsync(gameStreamObserver);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}