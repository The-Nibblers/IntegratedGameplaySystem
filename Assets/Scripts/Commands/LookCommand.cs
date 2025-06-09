using UnityEngine;

public class LookCommand : IDirectionalCommand
{
    private Player _player;

    public LookCommand(Player player)
    {
        _player = player;
    }

    public void Execute(Vector2 direction)
    {
        _player.Look(direction);
    }
}
