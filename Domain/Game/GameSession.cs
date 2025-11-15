using Domain.Game.Items;

namespace Domain.Game;

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
            throw new GameException("Game session is full");
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

    public Player? GetPlayerPlaying()
    {
        return _playerPlaying;
    }

    public BoardStatus PlaceCell(int column)
    {
        if (_playerPlaying is null)
        {
            throw new GameException("Game is not started");
        }
        
        BoardStatus boardStatus = _board.PlaceCell(column, _playerPlaying?.Id == Player1?.Id ? CellValue.Red : CellValue.Blue);
        _playerPlaying = _playerPlaying?.Id == Player1?.Id ? Player2 : Player1;
        return boardStatus;
    }
}