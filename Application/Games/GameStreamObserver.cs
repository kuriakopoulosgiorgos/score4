using Domain.Games.Boards;
using GrainInterfaces.Games;
using Orleans.Streams;

namespace Application.Games;

public class GameStreamObserver : IAsyncObserver<GameUpdateDto>
{
    public Task OnNextAsync(GameUpdateDto gameUpdate, StreamSequenceToken? token = null)
    {
        Console.WriteLine($"Player playing: {gameUpdate.PlayerPlaying?.Name}");
        Console.WriteLine($"Game Status: {gameUpdate.GameStatus}");
        Console.WriteLine($"Board Status: {gameUpdate.BoardStatus}");
        PrintCells(gameUpdate.Cells);
        return Task.CompletedTask;
    }

    public Task OnErrorAsync(Exception ex)
    {
        return Task.CompletedTask;
    }
    
    static void PrintCells(CellDto[,] cells)
    {
        for (int r = Board.Rows - 1 ; r >= 0; r--)
        {
            int startCursor = Console.CursorLeft;
            for (int c = 0; c < Board.Cols; c++)
            {
                Console.Write($"|\t{(int) cells[r, c].Value}\t");
                if (c == Board.Cols - 1)
                {
                    Console.Write("|");
                    int endCursor = Console.CursorLeft;
                    int length = endCursor - startCursor;
                    
                    Console.WriteLine();
                    
                    Console.WriteLine(new string('-', length));
                }
            }
        }
    }
}