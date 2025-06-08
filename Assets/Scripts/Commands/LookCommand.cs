using UnityEngine;

public class LookCommand : MonoBehaviour
{
    private Player _player;

    public LookCommand(Player player)
    {
        _player = player;
    }

    public void Execute(Vector3 direction)
    {
        _player.Look(direction);
    }
}
