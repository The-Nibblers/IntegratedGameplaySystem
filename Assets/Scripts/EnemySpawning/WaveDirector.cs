using UnityEngine;

public class WaveDirector
{
    private IWaveBuilder _waveBuilder;
    private Wave _currentWave;

    private Player _playerScript;
    
    /// <summary>
    /// TODO: Make more waves, wave decisions
    /// </summary>
    public WaveDirector(GameObject weakPrefab, GameObject mediumPrefab, GameObject strongPrefab, GameObject PlayerObject, Player PlayerScript)
    {
        _playerScript = PlayerScript;
        
        _waveBuilder = new WaveBuilder(weakPrefab, mediumPrefab, strongPrefab, PlayerObject, PlayerScript);
    }

    public void BuildFastWave()
    {
        _waveBuilder.BuildWeak(5);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }
    
    public void BuildMediumWave()
    {
        _waveBuilder.BuildMedium(5);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }

    public void BuildStrongWave()
    {
        _waveBuilder.BuildStrong(5);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }

    public void UpdateEnemies()
    {
        if (_currentWave == null) return;

        for (int i = _currentWave.Enemies.Count - 1; i >= 0; i--)
        {
            Enemy enemy = _currentWave.Enemies[i];
            enemy.EnemyUpdate();
        }
    }
    
    public bool IsWaveActive()
    {
        return _currentWave != null && _currentWave.WaveIsActive;
    }
}
