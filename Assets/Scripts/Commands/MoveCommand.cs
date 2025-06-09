using UnityEngine;

public class MoveCommand : IDirectionalCommand
{
    
    private Player _player;

    public MoveCommand(Player player)
    {
        _player = player;
    }

    public void Execute(Vector2 direction)
    {
        _player.Move(direction);
    }
}
