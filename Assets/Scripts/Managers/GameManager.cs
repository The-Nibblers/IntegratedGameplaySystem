using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player _player;
    
    [SerializeField] private GameObject _playerGameObject;
    void Start()
    {
        _player = new Player(_playerGameObject);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        _player.playerUpdate();
    }
}
