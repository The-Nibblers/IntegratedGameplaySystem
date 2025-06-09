using UnityEngine;

public class WaveDirector
{
    private IWaveBuilder _waveBuilder;
    
    /// <summary>
    /// TODO: Make more waves, update all enemies, wave decisions
    /// </summary>
    public WaveDirector(GameObject weakPrefab, GameObject mediumPrefab, GameObject strongPrefab, GameObject PlayerObject, Player PlayerScript)
    {
        _waveBuilder = new WaveBuilder(weakPrefab, mediumPrefab, strongPrefab, PlayerObject, PlayerScript);
    }

    public void BuildFastWave()
    {
        _waveBuilder.BuildWeak(5);
    }
    
    public void BuildMediumWave()
    {
        _waveBuilder.BuildMedium(5);
    }

    public void BuildStrongWave()
    {
        _waveBuilder.BuildStrong(5);
    }
}
