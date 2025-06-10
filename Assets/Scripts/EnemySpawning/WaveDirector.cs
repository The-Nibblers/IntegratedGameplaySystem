using UnityEngine;

public class WaveDirector
{
    private IWaveBuilder _waveBuilder;
    private Wave _currentWave;
    
    /// <summary>
    /// TODO: Make more waves, wave decisions
    /// </summary>
    public WaveDirector(GameObject weakPrefab, GameObject mediumPrefab, GameObject strongPrefab, GameObject PlayerObject, Player PlayerScript)
    {
        _waveBuilder = new WaveBuilder(weakPrefab, mediumPrefab, strongPrefab, PlayerObject, PlayerScript);
    }

    public void BuildFastWave()
    {
        _waveBuilder.BuildWeak(5);
        _currentWave = _waveBuilder.GetWave();
    }
    
    public void BuildMediumWave()
    {
        _waveBuilder.BuildMedium(5);
        _currentWave = _waveBuilder.GetWave();
    }

    public void BuildStrongWave()
    {
        _waveBuilder.BuildStrong(5);
        _currentWave = _waveBuilder.GetWave();
    }

    public void UpdateEnemies()
    {
        if (_currentWave == null) return;

        foreach (Enemy enemy in _currentWave.Enemies.ToArray())
        {
            enemy.EnemyUpdate();
        }
    }
    
    public bool IsWaveActive()
    {
        return _currentWave != null && _currentWave.WaveIsActive;
    }
}
