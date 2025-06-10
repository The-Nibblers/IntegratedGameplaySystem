using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player _player;
    [SerializeField] private GameObject _playerGameObject;

    [Header("Enemy prefabs")]
    [SerializeField] private GameObject _weakPrefab;
    [SerializeField] private GameObject _mediumPrefab;
    [SerializeField] private GameObject _strongPrefab;
    
    private WaveDirector _waveDirector;
    
    
    void Start()
    {
        _player = new Player(_playerGameObject);
        Cursor.lockState = CursorLockMode.Locked;
        _waveDirector = new WaveDirector(_weakPrefab, _mediumPrefab, _strongPrefab, _playerGameObject, _player);

        _waveDirector.BuildMediumWave();
    }
    
    void Update()
    {
        _player.playerUpdate();
        _waveDirector.UpdateEnemies();
    }
}
