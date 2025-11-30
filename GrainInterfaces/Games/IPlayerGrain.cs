namespace GrainInterfaces.Games;

[Alias("GrainInterfaces.Games.IPlayerGrain")]
public interface IPlayerGrain : IGrainWithStringKey
{
    Task SetName(string name);
    Task Join(IGameGrain gameGrain);
    Task<int> PlaceCell(int column);
    Task Exit();
}