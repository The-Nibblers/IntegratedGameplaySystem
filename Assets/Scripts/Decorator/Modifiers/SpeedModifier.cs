using UnityEngine;

public class SpeedModifier : StatsDecorator
{
    private float _multiplier;

    public SpeedModifier(IPlayerStats innerStats, float multiplier) : base(innerStats)
    {
        _multiplier = multiplier;
    }
    
    public override float GetMoveSpeed() => _innerStats.GetMoveSpeed() * _multiplier;
}
