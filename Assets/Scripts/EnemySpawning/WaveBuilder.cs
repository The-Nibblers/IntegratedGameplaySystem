using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveBuilder : IWaveBuilder
{
    private Wave _wave = new Wave();

    private GameObject _PlayerObject;
    private Player _playerScript;
    
    private GameObject _weakPrefab;
    private GameObject _MediumPrefab;
    private GameObject _StrongPrefab;

    private Vector3 _spawnLocation1 = new Vector3(-40, 0, 0);
    private Vector3 _spawnLocation2 = new Vector3(0, 0, 40);
    private Vector3 _spawnLocation3 = new Vector3(40, 0, 0);
    private List<Vector3> _spawnLocations = new List<Vector3>();

    public WaveBuilder(GameObject weakPrefab, GameObject mediumPrefab, GameObject strongPrefab, GameObject PlayerObject, Player PlayerScript)
    {
        _weakPrefab = weakPrefab;
        _MediumPrefab = mediumPrefab;
        _StrongPrefab = strongPrefab;
        _PlayerObject = PlayerObject;
        _playerScript = PlayerScript;
        
        _spawnLocations.Add(_spawnLocation1);
        _spawnLocations.Add(_spawnLocation2);
        _spawnLocations.Add(_spawnLocation3);
    }
    public void BuildWeak(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, _spawnLocations.Count);
            Vector3 spawnPosition = _spawnLocations[randomIndex];

            GameObject weakPrefabInstance = UnityEngine.Object.Instantiate(_weakPrefab, spawnPosition, Quaternion.identity);
            NavMeshAgent agent = weakPrefabInstance.GetComponent<NavMeshAgent>();
            _wave.AddEnemy(new Enemy(agent, _PlayerObject, _playerScript, weakPrefabInstance, 10, 4, 1, 1, 20, _wave));
        }
    }

    public void BuildMedium(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, _spawnLocations.Count);
            Vector3 spawnPosition = _spawnLocations[randomIndex];

            GameObject mediumPrefabInstance = UnityEngine.Object.Instantiate(_MediumPrefab, spawnPosition, Quaternion.identity);
            NavMeshAgent agent = mediumPrefabInstance.GetComponent<NavMeshAgent>();
            _wave.AddEnemy(new Enemy(agent, _PlayerObject, _playerScript, mediumPrefabInstance, 20, 2, 2, 1, 50, _wave));
        }
    }

    public void BuildStrong(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, _spawnLocations.Count);
            Vector3 spawnPosition = _spawnLocations[randomIndex];

            GameObject strongPrefabInstance = UnityEngine.Object.Instantiate(_StrongPrefab, spawnPosition, Quaternion.identity);
            NavMeshAgent agent = strongPrefabInstance.GetComponent<NavMeshAgent>();
            _wave.AddEnemy(new Enemy(agent, _PlayerObject, _playerScript, strongPrefabInstance, 40, 1, 4, 1, 100, _wave));
        }
    }

    public Wave GetWave()
    {
        return _wave;
    }
}
