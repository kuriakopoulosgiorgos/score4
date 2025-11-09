using Domain.Game.Items;

namespace Domain.Game;

public enum GameStatus
{
    NotStarted = 0,
    InProgress = 1,
    Player1Won = 2,
    Player2Won = 3,
    Draw = 4
}

public class GameSession
{
    public required string SessionId { get; init; }
    private readonly Board _board =  new ();
    private Player? Player1 { get; set; }
    private Player? Player2 { get; set;  }
    private Player? _playerPlaying;

    public void Join(Player player)
    {
        if (Player1 is not null && Player2 is not null)
        {
            return;
        }

        if (Player1 is null)
        {
            Player1 =  player;
            return;
        }
        
        Player2 = player;
        _playerPlaying = Random.Shared.Next(1, 2) == 1 ? Player1 : Player2;
    }

    public void PrintBoard()
    {
        _board.PrintBoard();
    }
    
    public GameStatus CheckGameStatus()
    {
        if (_playerPlaying is null)
        {
            return GameStatus.NotStarted;
        }
        
        if (_board.Has4Consecutive(CellValue.Red))
        {
            return GameStatus.Player1Won;
        }
        
        if (_board.Has4Consecutive(CellValue.Blue))
        {
            return GameStatus.Player2Won;
        }
    
        return _board.AnyCellEmpty() ? GameStatus.InProgress : GameStatus.Draw;
    }

    public Player? GetPlayerPlaying()
    {
        return _playerPlaying;
    }

    public int SetCellValue(int column)
    {
        if (CheckGameStatus()  == GameStatus.NotStarted)
        {
            throw new GameException("Game is not started");
        }
        
        int cellYPosition = _board.SetCellValue(column, _playerPlaying?.Id == Player1?.Id ? CellValue.Red : CellValue.Blue);
        if (cellYPosition is not -1)
        {
            _playerPlaying = _playerPlaying?.Id == Player1?.Id ? Player2 : Player1;
        }
        return cellYPosition;
    }
}