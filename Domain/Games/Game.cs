using Domain.Games.Boards;

namespace Domain.Games;

public class Game
{
    public required string RoomName { get; init; }
    private readonly Board _board =  new ();
    private Player? Player1 { get; set; }
    private Player? Player2 { get; set;  }
    private Player? _playerPlaying;
    private bool _isOpen;
    private int _playerOneScore;
    private int _playerTwoScore;
    
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
            Player1 = player;
        }
        else
        {
            Player2 = player;
        }
        
        _playerPlaying = Player1 is not null && Player2 is not null ?
            Random.Shared.Next(1, 2) == 1 ? Player1 : Player2
            : null;
        
        GameStatus gameStatus = _playerPlaying is not null ? GameStatus.Playing :  GameStatus.WaitingPlayersToJoin;
        OnGameUpdate(
            new GameUpdate(
                PlayerOneScore: _playerOneScore,
                PlayerTwoScore: _playerTwoScore,
                RoomName: RoomName,
                Cells: _board.Cells,
                PlayerPlaying: _playerPlaying,
                BoardStatus: BoardStatus.AvailableMoves,
                GameStatus: gameStatus
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

        if (boardStatus == BoardStatus.Player1Won)
        {
            _playerOneScore++;
        }
        
        if (boardStatus == BoardStatus.Player2Won)
        {
            _playerTwoScore++;
        }
        
        OnGameUpdate(
            new GameUpdate(
                PlayerOneScore: _playerOneScore,
                PlayerTwoScore: _playerTwoScore,
                RoomName: RoomName,
                Cells: _board.Cells,
                PlayerPlaying: _playerPlaying,
                BoardStatus: boardStatus,
                GameStatus: boardStatus == BoardStatus.AvailableMoves ? GameStatus.Playing : GameStatus.Finished
            )
        );
        return cellYPosition;
    }

    public void PlayAgain()
    {
        if (_board.CheckStatus() == BoardStatus.AvailableMoves)
        {
            throw new GameException("Game is not finished");
        }
        
        _board.Reset();
        
        _playerPlaying = Random.Shared.Next(1, 2) == 1 ? Player1 : Player2;
        
        OnGameUpdate(
            new GameUpdate(
                PlayerOneScore: _playerOneScore,
                PlayerTwoScore: _playerTwoScore,
                RoomName: RoomName,
                Cells: _board.Cells,
                PlayerPlaying: _playerPlaying,
                BoardStatus: BoardStatus.AvailableMoves,
                GameStatus: GameStatus.Playing
            )
        );
    }

    public void Exit(Player player)
    {
        if (!player.Equals(Player1) && !player.Equals(Player2))
        {
            throw new GameException($"Player: {player.Name} has not joined this game");
        }
        
        _board.Reset();
        _playerOneScore = 0;
        _playerTwoScore = 0;
        _playerPlaying = null;

        if (Player1?.Equals(player) ?? false)
        {
            Player1 = null;
        }

        if (Player2?.Equals(player) ?? false)
        {
            Player2 = null;
        }

        if (Player1 is null && Player2 is null)
        {
            _isOpen = false;
            return;
        }
        
        OnGameUpdate(
            new GameUpdate(
                PlayerOneScore: _playerOneScore,
                PlayerTwoScore: _playerTwoScore,
                RoomName: RoomName,
                Cells: _board.Cells,
                PlayerPlaying: _playerPlaying,
                BoardStatus: BoardStatus.AvailableMoves,
                GameStatus: GameStatus.WaitingPlayersToJoin
            )
        );
    }

    private void OnGameUpdate(GameUpdate gameUpdate)
    {
        GameUpdated?.Invoke(this, gameUpdate);
    }
}