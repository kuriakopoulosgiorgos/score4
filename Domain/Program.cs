using Domain.Game;
using Domain.Game.Items;

int selection;
GameSession gameSession = new GameSession
{
    SessionId = "1",
};

gameSession.Join(
    new Player
    {
        Id = "1",
        Name = "Geo"
    }
);

gameSession.Join(
    new Player
    {
        Id = "2",
        Name = "Jo"
    }
);

GameStatus gameStatus = gameSession.CheckGameStatus();

do
{
    gameSession.PrintBoard();
    Console.WriteLine($"Payer playing: {gameSession.GetPlayerPlaying()?.Name}");
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
        int cellYPosition = gameSession.SetCellValue(column);
        if (cellYPosition is -1)
        {
            Console.WriteLine("Column is full, could not add insert value to a cell");
        }
        
        gameStatus = gameSession.CheckGameStatus();
        Console.WriteLine($"Status: {nameof(gameStatus)}: {gameStatus}");
    }
} while (selection == 1 && GameStatus.InProgress == gameStatus);

gameSession.PrintBoard();
Console.WriteLine("Exiting...");