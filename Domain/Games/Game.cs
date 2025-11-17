using Domain.Games.Boards;

namespace Domain.Games;

public class Game
{
    public required string SessionId { get; init; }
    private readonly Board _board =  new ();
    private Player? Player1 { get; set; }
    private Player? Player2 { get; set;  }
    private Player? _playerPlaying;
    private bool _isOpen;
    
    public event EventHandler<GameUpdate>? GameUpdated;

    public void Open()
    {
        if (_isOpen)
        {
            throw new GameException("Game already opened");
        }
        _isOpen = true;
    }

    public void Join(Player player)
    {
        if (!_isOpen)
        {
            throw new GameException("Game is not open");
        }
        
        if (Player1 is not null && Player2 is not null)
        {
            throw new GameException("Game session is full");
        }

        if (Player1 is null)
        {
            Player1 =  player;
            OnGameUpdate(
                new GameUpdate(
                    Cells: _board.Cells,
                    PlayerPlaying: null,
                    BoardStatus: BoardStatus.AvailableMoves,
                    GameStatus: GameStatus.WaitingPlayersToJoin
                )
            );
            return;
        }
        
        Player2 = player;
        _playerPlaying = Random.Shared.Next(1, 2) == 1 ? Player1 : Player2;
        OnGameUpdate(
            new GameUpdate(
                Cells: _board.Cells,
                PlayerPlaying: _playerPlaying,
                BoardStatus: BoardStatus.AvailableMoves,
                GameStatus: GameStatus.Playing
            )
        );
    }

    public int PlaceCell(Player player, int column)
    {
        if (_board.CheckStatus() != BoardStatus.AvailableMoves)
        {
            throw new GameException("Game is finished");
        }
        if (_playerPlaying is null)
        {
            throw new GameException("Game is not started");
        }

        if (!player.Equals(_playerPlaying))
        {
            throw new GameException($"Player: {player.Name} is not playing");
        }
        
        int cellYPosition = _board.PlaceCell(column, _playerPlaying?.Id == Player1?.Id ? CellValue.Red : CellValue.Blue);
        _playerPlaying = _playerPlaying?.Id == Player1?.Id ? Player2 : Player1;
        BoardStatus boardStatus = _board.CheckStatus();
        OnGameUpdate(
            new GameUpdate(
                Cells: _board.Cells,
                PlayerPlaying: _playerPlaying,
                BoardStatus: boardStatus,
                GameStatus: boardStatus == BoardStatus.AvailableMoves ? GameStatus.Playing : GameStatus.Finished
            )
        );
        return cellYPosition;
    }

    private void OnGameUpdate(GameUpdate gameUpdate)
    {
        GameUpdated?.Invoke(this, gameUpdate);
    }
}