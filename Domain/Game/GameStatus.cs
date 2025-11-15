using Domain.Game.Items;

namespace Domain.Game;

public enum Status
{
    InProgress = 1,
    Player1Won = 2,
    Player2Won = 3,
    Draw = 4
}

public class BoardStatus()
{
    public required Status Status { get; init; }
    public required Cell[,] Cells { get; init; }
}
