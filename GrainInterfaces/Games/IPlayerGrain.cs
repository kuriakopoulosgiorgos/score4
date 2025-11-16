namespace GrainInterfaces.Games;

[Alias("GrainInterfaces.Games.IPlayerGrain")]
public interface IPlayerGrain : IGrainWithGuidKey
{
    Task SetName(string name);
    Task Join(IGameGrain gameGrain);
    Task<int> Play(int column);
}