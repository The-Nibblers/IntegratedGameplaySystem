using System.Collections.Generic;
using UnityEngine;

public class WaveDirector
{
    private IWaveBuilder _waveBuilder;
    private Wave _currentWave;

    private Player _playerScript;
    private ItemDropper _itemDropper;

    private bool _isEarlyGame;
    private bool _isMidGame;
    private bool _isEndGame;
    private bool _isPlayerDyingWaves;
    
    /// <summary>
    /// TODO: Make more waves, wave decisions
    /// </summary>
    public WaveDirector(GameObject weakPrefab, GameObject mediumPrefab, GameObject strongPrefab, GameObject PlayerObject, Player PlayerScript, List<GameObject> itemPrefabs)
    {
        _playerScript = PlayerScript;
        
        _itemDropper = new ItemDropper(itemPrefabs, PlayerScript);
        
        _waveBuilder = new WaveBuilder(weakPrefab, mediumPrefab, strongPrefab, PlayerObject, PlayerScript, _itemDropper);
    }

    //early game waves
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
    
    //mid game waves
    public void BuildStrongFastWave()
    {
        _waveBuilder.BuildWeak(7);
        _waveBuilder.BuildStrong(5);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }
    
    public void BuildStrongMedWave()
    {
        _waveBuilder.BuildStrong(5);
        _waveBuilder.BuildMedium(7);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }
    
    public void BuildMedFastWave()
    {
        _waveBuilder.BuildWeak(3);
        _waveBuilder.BuildMedium(10);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }

    public void BuildVeryStrongWave()
    {
        _waveBuilder.BuildStrong(14);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }

    public void BuildVeryFastWave()
    {
        _waveBuilder.BuildWeak(15);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }
    
    //end game waves

    public void BuildEndWeakWave()
    {
        _waveBuilder.BuildWeak(10);
        _waveBuilder.BuildMedium(6);
        _waveBuilder.BuildStrong(3);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }

    public void BuildEndMediumWave()
    {
        _waveBuilder.BuildWeak(5);
        _waveBuilder.BuildMedium(10);
        _waveBuilder.BuildStrong(4);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }

    public void BuildEndStrongWave()
    {
        _waveBuilder.BuildWeak(5);
        _waveBuilder.BuildMedium(4);
        _waveBuilder.BuildStrong(10);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }
    
    //YOU WILL DIE WAVE
    public void BuildFinalWave()
    {
        _waveBuilder.BuildWeak(100);
        _waveBuilder.BuildMedium(100);
        _waveBuilder.BuildStrong(100);
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
        
        _itemDropper.ItemUpdate();
    }
    
    public bool IsWaveActive()
    {
        return _currentWave != null && _currentWave.WaveIsActive;
    }
}
