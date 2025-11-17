using Domain.Games.Boards;

namespace Domain.Games;

public record GameUpdate
(
    Cell[,] Cells, 
    Player? PlayerPlaying, 
    BoardStatus BoardStatus,
    GameStatus GameStatus
);
