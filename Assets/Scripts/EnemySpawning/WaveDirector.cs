using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    private bool _isFinalWave;
    
    private List<Action> _earlyGameWaves;
    private List<Action> _midGameWaves;
    private List<Action> _endGameWaves;
    private List<Action> _finalWaves;
    
    private int WaveCounter = 0;
    
    public WaveDirector(GameObject weakPrefab, GameObject mediumPrefab, GameObject strongPrefab, GameObject PlayerObject, Player PlayerScript, List<GameObject> itemPrefabs)
    {
        _playerScript = PlayerScript;
        
        _itemDropper = new ItemDropper(itemPrefabs, PlayerScript);
        
        _waveBuilder = new WaveBuilder(weakPrefab, mediumPrefab, strongPrefab, PlayerObject, PlayerScript, _itemDropper);
        
        _earlyGameWaves = new List<Action>
        {
            BuildFastWave,
            BuildMediumWave,
            BuildStrongWave
        };

        _midGameWaves = new List<Action>
        {
            BuildStrongFastWave,
            BuildStrongMedWave,
            BuildMedFastWave,
            BuildVeryStrongWave,
            BuildVeryFastWave
        };

        _endGameWaves = new List<Action>
        {
            BuildEndWeakWave,
            BuildEndMediumWave,
            BuildEndStrongWave
        };

        _finalWaves = new List<Action>
        {
            BuildFinalWave
        };
    }

    private void SetWavePhase()
    {
        _isEarlyGame = false;
        _isMidGame = false;
        _isEndGame = false;
        _isFinalWave = false;

        if (WaveCounter == 1 || WaveCounter == 2)
        {
            _isEarlyGame = true;
        }
        else if (WaveCounter >= 3 && WaveCounter <= 5)
        {
            _isMidGame = true;
        }
        else if (WaveCounter == 6 || WaveCounter == 7)
        {
            _isEndGame = true;
        }
        else if (WaveCounter >= 8)
        {
            _isFinalWave = true;
        }
    }
    public void SpawnWave()
    {
        if (WaveCounter == 0)
        {
            BuildFastWave();
            WaveCounter++;
            return;
        }

        SetWavePhase();
        
        if (_isEarlyGame)
        {
            _earlyGameWaves[UnityEngine.Random.Range(0, _earlyGameWaves.Count)]();
            WaveCounter++;
        }
        else if (_isMidGame)
        {
            _midGameWaves[UnityEngine.Random.Range(0, _midGameWaves.Count)]();
            WaveCounter++;
        }
        else if (_isEndGame)
        {
            _endGameWaves[UnityEngine.Random.Range(0, _endGameWaves.Count)]();
            WaveCounter++;
        }
        else if (_isFinalWave)
        {
            _finalWaves[UnityEngine.Random.Range(0, _finalWaves.Count)]();
            WaveCounter++;
        }
    }
    
    //early game waves
    private void BuildFastWave()
    {
        _waveBuilder.BuildWeak(4);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }
    
    private void BuildMediumWave()
    {
        _waveBuilder.BuildMedium(3);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }

    private void BuildStrongWave()
    {
        _waveBuilder.BuildStrong(2);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }
    
    //mid game waves
    private void BuildStrongFastWave()
    {
        _waveBuilder.BuildWeak(7);
        _waveBuilder.BuildStrong(3);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }
    
    private void BuildStrongMedWave()
    {
        _waveBuilder.BuildStrong(3);
        _waveBuilder.BuildMedium(7);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }
    
    private void BuildMedFastWave()
    {
        _waveBuilder.BuildWeak(3);
        _waveBuilder.BuildMedium(10);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }

    private void BuildVeryStrongWave()
    {
        _waveBuilder.BuildStrong(13);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }

    private void BuildVeryFastWave()
    {
        _waveBuilder.BuildWeak(10);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }
    
    //end game waves

    private void BuildEndWeakWave()
    {
        _waveBuilder.BuildWeak(10);
        _waveBuilder.BuildMedium(6);
        _waveBuilder.BuildStrong(3);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }

    private void BuildEndMediumWave()
    {
        _waveBuilder.BuildWeak(5);
        _waveBuilder.BuildMedium(10);
        _waveBuilder.BuildStrong(4);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }

    private void BuildEndStrongWave()
    {
        _waveBuilder.BuildWeak(5);
        _waveBuilder.BuildMedium(4);
        _waveBuilder.BuildStrong(10);
        _currentWave = _waveBuilder.GetWave();
        _playerScript.PlayerSetWave(_currentWave);
    }
    
    //YOU WILL DIE WAVE
    private void BuildFinalWave()
    {
        _waveBuilder.BuildWeak(50);
        _waveBuilder.BuildMedium(50);
        _waveBuilder.BuildStrong(50);
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
        bool active = _currentWave != null && _currentWave.WaveIsActive;
        return active;
    }
}
