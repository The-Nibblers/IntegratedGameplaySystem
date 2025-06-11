using UnityEngine;

public class QuitCommand : ICommand
{
    private Player _player;

    public QuitCommand(Player player)
    {
        _player = player;
    }

    public void Execute()
    {
        _player.QuitGame();
    }
}
