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

    private Vector3 _spawnLocation1 = new Vector3(-40, 0.8f, 0);
    private Vector3 _spawnLocation2 = new Vector3(0, 0.8f, 40);
    private Vector3 _spawnLocation3 = new Vector3(40, 0.8f, 0);
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
            for (int j = 0; j < _spawnLocations.Count; j++)
            {
                int random = Random.Range(0, _spawnLocations.Count);
                if (j == random)
                {
                    GameObject weakPrefabIntance = UnityEngine.Object.Instantiate(_weakPrefab, _spawnLocations[j], Quaternion.identity);
                    _wave.AddEnemy((new Enemy(_weakPrefab.GetComponent<NavMeshAgent>(), _PlayerObject, _playerScript, weakPrefabIntance,  10, 4,1, 1, 20, _wave)));
                }
            }
        }
    }

    public void BuildMedium(int count)
    {
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < _spawnLocations.Count; j++)
            {
                int random = Random.Range(0, _spawnLocations.Count);
                if (j == random)
                {
                    GameObject mediumPrefabInstance = UnityEngine.Object.Instantiate(_MediumPrefab, _spawnLocations[j], Quaternion.identity);
                    _wave.AddEnemy((new Enemy(_MediumPrefab.GetComponent<NavMeshAgent>(), _PlayerObject, _playerScript, mediumPrefabInstance, 20, 2,2, 1, 50, _wave)));
                }
            }
        }
    }

    public void BuildStrong(int count)
    {
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < _spawnLocations.Count; j++)
            {
                int random = Random.Range(0, _spawnLocations.Count);
                if (j == random)
                {
                    GameObject strongPrefabInstance = UnityEngine.Object.Instantiate(_StrongPrefab, _spawnLocations[j], Quaternion.identity);
                    _wave.AddEnemy((new Enemy(_StrongPrefab.GetComponent<NavMeshAgent>(), _PlayerObject, _playerScript, strongPrefabInstance, 40, 1,4, 1, 100, _wave)));
                }
            }
        }
    }

    public Wave GetWave()
    {
        return _wave;
    }
}
