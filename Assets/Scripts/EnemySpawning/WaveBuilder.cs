using UnityEngine;
using UnityEngine.AI;

public class WaveBuilder : IWaveBuilder
{
    private Wave _wave = new Wave();

    private GameObject _PlayerObject;
    private Player _playerScript;
    
    private GameObject WeakPrefab;
    private GameObject MediumPrefab;
    private GameObject StrongPrefab;
    
   /// <summary>
   /// TODO: instantiate prefabs when building
   /// </summary>
   /// <param name="count"></param>

    public void BuildWeak(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _wave.AddEnemy((new Enemy(WeakPrefab.GetComponent<NavMeshAgent>(), _PlayerObject, _playerScript, WeakPrefab,  10, 4,1, 1, 20, _wave)));
        }
    }

    public void BuildMedium(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _wave.AddEnemy((new Enemy(MediumPrefab.GetComponent<NavMeshAgent>(), _PlayerObject, _playerScript, MediumPrefab, 20, 2,2, 1, 50, _wave)));
        }
    }

    public void BuildStrong(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _wave.AddEnemy((new Enemy(StrongPrefab.GetComponent<NavMeshAgent>(), _PlayerObject, _playerScript, StrongPrefab, 40, 1,4, 1, 100, _wave)));
        }
    }

    public Wave GetWave()
    {
        return _wave;
    }
}
