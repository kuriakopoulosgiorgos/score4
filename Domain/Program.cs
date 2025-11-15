using Domain.Game;
using Domain.Game.Items;

int selection;
GameSession gameSession = new GameSession
{
    SessionId = "1",
};

gameSession.Join(
    new Player
    (
        Id: "1",
        Name: "Geo"
    )
);

gameSession.Join(
    new Player
    (
        Id: "2",
        Name: "Jo"
    )
);

BoardStatus boardStatus = new BoardStatus
{
    Cells = new Cell[1, 1],
    Status = Status.InProgress
};

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

        try
        {
            boardStatus = gameSession.PlaceCell(column);
            Console.WriteLine($"Status: {nameof(boardStatus.Status)}: {boardStatus.Status}");
        }
        catch (GameException e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
    }
} while (selection == 1 && Status.InProgress == boardStatus.Status);

gameSession.PrintBoard();
Console.WriteLine("Exiting...");