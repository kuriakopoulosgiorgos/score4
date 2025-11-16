namespace Domain.Games.Boards;

public enum CellValue
{
    Empty = 0,
    Red = 1,
    Blue = 2,
}

public class Cell
{
    public required CellValue Value { get; set; }
}