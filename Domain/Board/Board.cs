namespace Game.Board;

public enum BoardStatus
{
    InProgress = 0,
    RedWon = 1,
    BlueWon = 2,
    Draw = 3
}

public class Board
{
    public const int Rows = 6, Cols = 7;
    
    private readonly Cell[,] _cells = new Cell[Rows, Cols];

    public Board()
    {
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Cols; c++)
            {
                _cells[r, c] = new Cell
                {
                    Value = CellValue.Empty
                };
            }
        }
    }

    public CellValue GetCellValue(int x, int y)
    {
        return _cells[x, y].Value;
    }

    public int SetCellValue(int column, CellValue cellValue)
    {
        var cellYPosition = 0;
        while (cellYPosition < Rows && GetCellValue(cellYPosition, column) != CellValue.Empty)
        {
            cellYPosition++;
        }

        if (cellYPosition >= Rows)
        {
            return -1;
        }
        
        _cells[cellYPosition, column].Value = cellValue;
        return cellYPosition;
    }

    public BoardStatus CheckBoardStatus()
    {
        if (IsWinner(CellValue.Red))
        {
            return BoardStatus.RedWon;
        }
        
        if (IsWinner(CellValue.Blue))
        {
            return BoardStatus.BlueWon;
        }
    
        return AnyCellEmpty() ? BoardStatus.InProgress : BoardStatus.Draw;
    }

    public void PrintBoard()
    {
        for (int r = Rows - 1 ; r >= 0; r--)
        {
            int startCursor = Console.CursorLeft;
            for (int c = 0; c < Cols; c++)
            {
                Console.Write($"|\t{(int) _cells[r, c].Value}\t");
                if (c == Cols - 1)
                {
                    Console.Write("|");
                    // Find how many columns we used
                    int endCursor = Console.CursorLeft;
                    int length = endCursor - startCursor;

                    // Move to the next line
                    Console.WriteLine();

                    // Write a line of dashes of the same length
                    Console.WriteLine(new string('-', length));
                }
            }
        }
    }

    private bool IsWinner(CellValue cellValue)
    {
        for (int line = 0; line < Rows; line++)
        {
            if (ConsecutiveFourHorizontal(line, cellValue))
            {
                return true;
            }
        }
        
        for (int column = 0; column < Cols; column++)
        {
            if (ConsecutiveFourVertical(column, cellValue))
            {
                return true;
            }
        }

        if (ConsecutiveFourInDiagonals(cellValue))
        {
            return true;
        }
        
        return false;
    }
    
    private bool ConsecutiveFourHorizontal(int line, CellValue cellValue)
    {
        return ConsecutiveFour(Cols, i => _cells[line, i], cellValue);
    }
    
    private bool ConsecutiveFourVertical(int column, CellValue cellValue)
    {
        return ConsecutiveFour(Rows, i => _cells[i, column], cellValue);
    }
    
    private bool ConsecutiveFour(int length, Func<int, Cell> cellSupplier, CellValue cellValue)
    {
        int count = 0;

        for (int i = 0; i < length; i++)
        {
            if (cellSupplier(i).Value == cellValue)
            {
                count++;
                if (count == 4)
                {
                    return true;
                }
            }
            else
            {
                count = 0;
                if (length - i < 4)
                {
                    break;
                }
            }
        }

        return false;
    }
    
    bool ConsecutiveFourInDiagonals(CellValue cellValue)
    {
        for (int r = 0; r <= Rows - 4; r++)
        {
            for (int c = 0; c <= Cols - 4; c++)
            {
                if (_cells[r, c].Value == cellValue &&
                    _cells[r + 1, c + 1].Value == cellValue &&
                    _cells[r + 2, c + 2].Value == cellValue &&
                    _cells[r + 3, c + 3].Value == cellValue)
                {
                    return true;
                }
            }
        }

        for (int r = 0; r <= Rows - 4; r++)
        {
            for (int c = 3; c < Cols; c++)
            {
                if (_cells[r, c].Value == cellValue &&
                    _cells[r + 1, c - 1].Value == cellValue &&
                    _cells[r + 2, c - 2].Value == cellValue &&
                    _cells[r + 3, c - 3].Value == cellValue)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool AnyCellEmpty()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                if (_cells[i, j].Value == CellValue.Empty)
                {
                    return true;
                }
            }
        }
        
        return false;
    }
}