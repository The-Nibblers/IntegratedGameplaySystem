using UnityEngine;

public class ShootCommand : ICommand
{
    private Player _player;

    public ShootCommand(Player player)
    {
        _player = player;
    }

    public void Execute()
    {
        _player.Shoot();
    }
}
