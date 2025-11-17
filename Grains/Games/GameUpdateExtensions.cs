using Domain.Games;
using Domain.Games.Boards;
using GrainInterfaces.Games;
using BoardStatus = GrainInterfaces.Games.BoardStatus;
using CellValue = GrainInterfaces.Games.CellValue;
using GameStatus = GrainInterfaces.Games.GameStatus;
namespace Grains.Games;

public static class GameUpdateExtensions
{
    public static GameUpdateDto ToGameUpdateDto(this GameUpdate gameUpdate)
    {
        Cell[,] source = gameUpdate.Cells;
        int rows = source.GetLength(0);
        int cols = source.GetLength(1);
        CellDto[,] destination = new CellDto[rows, cols];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                destination[r, c] = new CellDto((CellValue) source[r, c].Value);
            }
        }

        PlayerDto? playerPlaying = gameUpdate.PlayerPlaying is not null
            ? new PlayerDto(
                Id: gameUpdate.PlayerPlaying.Id,
                Name: gameUpdate.PlayerPlaying.Name
            ) : null;

        return new GameUpdateDto(
            Cells: destination,
            PlayerPlaying: playerPlaying,
            BoardStatus: (BoardStatus) gameUpdate.BoardStatus,
            GameStatus: (GameStatus) gameUpdate.GameStatus
        );
    }
}