using UnityEngine;

public class WaveDirector
{
    private IWaveBuilder _waveBuilder;

    public WaveDirector()
    {
        _waveBuilder = new WaveBuilder();
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
    
    //make more waves
}
