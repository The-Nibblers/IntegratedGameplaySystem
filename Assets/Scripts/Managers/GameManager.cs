using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player _player;
    void Start()
    {
        _player = new Player();
    }

    // Update is called once per frame
    void Update()
    {
        _player.playerUpdate();
    }
}
