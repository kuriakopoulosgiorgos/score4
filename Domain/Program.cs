using Domain.Games;
using Domain.Games.Boards;

int selection;
Game game = new Game
{
    SessionId = "1",
};

Player player1 = new
(
    Id: "1",
    Name: "Player 1"
);

Player player2 = new
(
    Id: "2",
    Name: "Player 2"
);

Player playerPlaying = player1;
BoardStatus boardStatus = BoardStatus.AvailableMoves;
GameStatus gameStatus = GameStatus.WaitingPlayersToJoin;
Cell[,] cells =  new Cell[Board.Rows, Board.Cols];
game.GameUpdated += (sender, gameUpdate) =>
{
    playerPlaying = gameUpdate.PlayerPlaying ?? playerPlaying;
    boardStatus = gameUpdate.BoardStatus;
    gameStatus = gameUpdate.GameStatus;
    cells = gameUpdate.Cells;
};

game.Join(player1);
game.Join(player2);

do
{
    PrintCells(cells);
    Console.WriteLine($"Player playing: {playerPlaying?.Name ?? ""}");
    Console.WriteLine($"To insert value to a cell press: 1");
    Console.WriteLine($"Press any other key to exit");
    if (!int.TryParse(Console.ReadLine(), out selection))
    {
        selection = 0;
    }
    if (selection == 1)
    {
        int column;
        do
        {
            Console.WriteLine($"Select column to insert value to a cell: ");
            if (!int.TryParse(Console.ReadLine(), out column))
            {
                column = -1;
            }
            if (column is < 0 or >= Board.Cols)
            {
                Console.WriteLine($"Value must be a number between 0 and {Board.Cols - 1}");
            }
        } while (column is < 0 or >= Board.Cols);

        try
        {
            game.PlaceCell(playerPlaying, column);
            Console.WriteLine($"Status: {nameof(gameStatus)}: {gameStatus}");
        }
        catch (GameException e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
    }
} while (selection == 1 && GameStatus.Playing == gameStatus);

PrintCells(cells);
Console.WriteLine($"Board status: {boardStatus}");
Console.WriteLine("Exiting...");

static void PrintCells(Cell[,] cells)
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
