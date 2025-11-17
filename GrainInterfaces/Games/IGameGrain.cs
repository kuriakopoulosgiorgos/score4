namespace GrainInterfaces.Games;

[Alias("GrainInterfaces.Games.IGameGrain")]
public interface IGameGrain : IGrainWithStringKey
{
    Task Open();
    
    Task Join(PlayerDto playerDto);
    
    public Task<int> PlaceCell(PlayerDto player, int column);
}