using UnityEngine;

public interface IWaveBuilder
{
    void BuildWeak(int count);
    void BuildMedium(int count);
    void BuildStrong(int count);
    Wave GetWave();
}
