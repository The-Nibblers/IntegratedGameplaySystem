using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private GameObject _deathUI;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _quitButton;

    [SerializeField] private GameObject _startMenu;
    
    private Dictionary<string, TextMeshProUGUI> _uiItems = new Dictionary<string, TextMeshProUGUI>();
    private UIManager _uiManager;

    [Header("GunAnimator")] [SerializeField]
    private Animator _gunAnimator;
    
    private bool _waveSpawning = false;
    
    private WaveDirector _waveDirector;
    
    
    void Start()
    {
        foreach (var entry in _uiItemList)
        {
            if (!_uiItems.ContainsKey(entry.key))
            {
                _uiItems.Add(entry.key, entry.value);
            }
        }
        
        _uiManager = new UIManager(_uiItems, _deathUI, _restartButton, _quitButton);
        _player = new Player(_playerGameObject, _uiManager, _gunAnimator);
        
        _waveDirector = new WaveDirector(_weakPrefab, _mediumPrefab, _strongPrefab, _playerGameObject, _player, _itemPrefabs);
    }
    
    void Update()
    {
        _player.playerUpdate();
        _waveDirector.UpdateEnemies();

        if (!_waveDirector.IsWaveActive() && !_waveSpawning)
        {
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        _waveSpawning = true;
        yield return new WaitForSeconds(2);
        _waveDirector.SpawnWave();
        _waveSpawning = false;
    }

    public void StartGameButton()
    {
        _startMenu.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        if (!_waveDirector.IsWaveActive() && !_waveSpawning)
        {
            StartCoroutine(SpawnWave());   
        }
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
