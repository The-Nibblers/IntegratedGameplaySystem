using UnityEngine;

public class FireRateModifier : StatsDecorator
{
    private float _multiplier;

    public FireRateModifier(IPlayerStats innerStats, float multiplier) : base(innerStats)
    {
        _multiplier = multiplier;
    }

    public override float GetFireRate() => _innerStats.GetFireRate() * _multiplier;
}
