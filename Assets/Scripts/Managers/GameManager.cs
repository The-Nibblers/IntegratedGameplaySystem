using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player _player;
    [SerializeField] private GameObject _playerGameObject;

    [Header("Enemy prefabs")]
    [SerializeField] private GameObject _weakPrefab;
    [SerializeField] private GameObject _mediumPrefab;
    [SerializeField] private GameObject _strongPrefab;
    
    [Header("Item prefabs")]
    [SerializeField] private List<GameObject> _itemPrefabs;
    
    [Header("UI Items")]
    [SerializeField] private List<GameObject> _uiItems;
    private UIManager _uiManager;
    
    private WaveDirector _waveDirector;
    
    
    void Start()
    {
        _player = new Player(_playerGameObject);
        Cursor.lockState = CursorLockMode.Locked;
        _waveDirector = new WaveDirector(_weakPrefab, _mediumPrefab, _strongPrefab, _playerGameObject, _player, _itemPrefabs);

        _waveDirector.BuildFastWave();
    }
    
    void Update()
    {
        _player.playerUpdate();
        _waveDirector.UpdateEnemies();

        if (!_waveDirector.IsWaveActive())
        {
            _waveDirector.BuildMediumWave();
        }
    }
}
