using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private List<UIItemEntry> _uiItemList = new List<UIItemEntry>();
    private Dictionary<string, TextMeshProUGUI> _uiItems = new Dictionary<string, TextMeshProUGUI>();
    private UIManager _uiManager;

    [Header("GunAnimator")] [SerializeField]
    private Animator _gunAnimator;
    
    private WaveDirector _waveDirector;
    
    
    void Start()
    {
        foreach (var entry in _uiItemList)
        {
            if (!_uiItems.ContainsKey(entry.key))
            {
                _uiItems.Add(entry.key, entry.value);
            }
            else
            {
                Debug.LogWarning("Duplicate key: " + entry.key);
            }
        }
        
        _uiManager = new UIManager(_uiItems);
        _player = new Player(_playerGameObject, _uiManager, _gunAnimator);
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
