using UnityEngine;

public class MoveCommand : IDirectionalCommand
{
    
    private Player _player;

    public MoveCommand(Player player)
    {
        _player = player;
    }

    public void Execute(Vector3 direction)
    {
        _player.Move(direction);
    }
}
