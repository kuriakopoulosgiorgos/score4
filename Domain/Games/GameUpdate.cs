using Domain.Games.Boards;

namespace Domain.Games;

public record GameUpdate
(
    string RoomName,
    Cell[,] Cells, 
    Player? PlayerPlaying, 
    BoardStatus BoardStatus,
    GameStatus GameStatus
);
