namespace GrainInterfaces.Games;

public enum CellValue
{
    Empty = 0,
    Red = 1,
    Blue = 2,
}

[GenerateSerializer, Immutable]
public record CellDto(CellValue Value);

public enum BoardStatus
{
    AvailableMoves = 1,
    Player1Won = 2,
    Player2Won = 3,
    Draw = 4
}

public enum GameStatus
{
    WaitingPlayersToJoin = 1,
    Playing = 2,
    Finished = 3
}


[GenerateSerializer, Immutable]
public record GameUpdateDto
(
    string RoomName,
    CellDto[,] Cells, 
    PlayerDto? PlayerPlaying, 
    BoardStatus BoardStatus,
    GameStatus GameStatus
);
