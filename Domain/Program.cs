using Game.Board;

Board board = new();

int selection;
BoardStatus boardStatus = BoardStatus.InProgress;
CellValue cellValue = CellValue.Red;
do
{
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
        int cellYPosition = board.SetCellValue(column, cellValue);
        if (cellYPosition is -1)
        {
            Console.WriteLine("Column is full, could not add insert value to a cell");
        }
        board.PrintBoard();

        boardStatus = board.CheckBoardStatus();
        Console.WriteLine($"Status: {nameof(boardStatus)}: {boardStatus})");
        cellValue = cellValue == CellValue.Red ? CellValue.Blue :  CellValue.Red;
    }
} while (selection == 1 && BoardStatus.InProgress == boardStatus);

Console.WriteLine($"Exiting...");