using Domain.Games.Boards;

namespace Domain.Games;

public record GameUpdate(
    int PlayerOneScore,
    int PlayerTwoScore,
    string RoomName,
    Cell[,] Cells, 
    Player? PlayerPlaying, 
    BoardStatus BoardStatus,
    GameStatus GameStatus
);
